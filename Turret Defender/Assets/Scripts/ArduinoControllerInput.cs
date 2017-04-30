using UnityEngine;
//Using statements for Serial Port
using System.IO.Ports;
using System.Collections;
//System is also needed.
using System;
using System.Text;

public class ArduinoControllerInput : MonoBehaviour
{
    //Public string for changing the port, we could enumrate and the Ports
    //but this is fine for an example

    public float horizontalRestingValue = 512f;
    public float verticalRestingValue = 510f;

    public float verticalMaxValue;
    public float zoomMaxValue;

    public float minFovZoomValue;
    public float maxFovZoomValue;

    public float horizontalDeadzone;
    public float verticalDeadzone;

    private float horizontalInput;
    private float verticalInput;
    private float zoomInput;
    private int fireSwitchInput;
    private int viewSwitchInput;
    private bool connected;

    //Serial Port, this will hold the instance of the serial Port
    //This is the main way to communicate with the serial device
    SerialPort serialPort;


    // Use this for initialization
    void Start()
    {
        //Initialise the serial port
        serialPort = new SerialPort();
        //Collate a list of all available portnames.
        string[] comPorts = SerialPort.GetPortNames();
        //Parity bits, this is used for error checking and must be agreed on by the device
        //and the host. In this case we don't use it!
        serialPort.Parity = Parity.None;
        //The Baud Rate is set to 9600, this is the default for mostn serial coms
        serialPort.BaudRate = 9600;
        //serialPort.BaudRate = 11150;
        //This is the length of bits communicated between the devices, the default is 8
        serialPort.DataBits = 8;
        //The stop bits in the data, per byte
        serialPort.StopBits = StopBits.One;
        //Decides amount of miliseconds before a write attempt times out.
        serialPort.WriteTimeout = 1;

        //Opens first available port, if successful breaks.
        foreach (string port in comPorts)
        {
            serialPort.PortName = port;
            serialPort.Open();
            connected = serialPort.IsOpen;
            if (connected)
            {
                break;
            }
        }

    }



    // Update is called once per frame
    void Update()
    {
       
            //Reads from the arduino and updates input base values.
            UpdateArduinoInput();

            //Normalizes incoming horizontal input.
            horizontalInput = NormalizeMotorInput(horizontalInput, horizontalRestingValue);
        
    }

    public void SendData(string data)
    {
        
        //Sends a string to the arduino, used for loading LED's.
        serialPort.Write(data);
        serialPort.BaseStream.Flush();
        serialPort.DiscardOutBuffer();

    }

    void UpdateArduinoInput()
    {
        //Gets full string of all data.
        string data = serialPort.ReadLine();

        //Splits string into segments.
        string[] controllerInput = data.Split(',');

        //Store each segment in variables, convert to appropriate type.
        //TODO Try parse.
        horizontalInput = float.Parse(controllerInput[0]);
        verticalInput = float.Parse(controllerInput[1]);
        viewSwitchInput = int.Parse(controllerInput[2]);
        fireSwitchInput = int.Parse(controllerInput[3]);
        zoomInput = float.Parse(controllerInput[4]);

    }

    float NormalizeMotorInput(float input, float baseValue)
    {

        //Takes resting value of motor input to 1, then minimizes for control purporses.
        input -= baseValue;
        input /= baseValue;

        return input;

    }

    public float GetHorizontalInput()
    {

        //Disables input if its less than deadzone value.
        if (Mathf.Abs(horizontalInput) < horizontalDeadzone)
        {
            horizontalInput = 0f;
        }

        return horizontalInput;

    }

    public float GetVerticalInput()
    {

        //Uses the hinges as the limits for potentiometer conversion.
        float barrelHingeMinLimit = gameObject.GetComponentInChildren<HingeJoint>().limits.min;
        float barrelHingeMaxLimit = gameObject.GetComponentInChildren<HingeJoint>().limits.max;

        //Converts the input to fit within requirements before sending.
        float convertedVerticalInput = ConvertPotentiometer(verticalInput, barrelHingeMinLimit, barrelHingeMaxLimit, verticalMaxValue);

        return convertedVerticalInput;

    }

    public int GetFireSwitchInput()
    {

        return fireSwitchInput;

    }

    public int GetViewSwitchInput()
    {

        return viewSwitchInput;

    }

    public float GetZoomInput()
    {
        
        float convertedZoomInput = ConvertPotentiometer(zoomInput, minFovZoomValue, maxFovZoomValue, zoomMaxValue);

        return convertedZoomInput;

    }

    float ConvertPotentiometer(float input, float minTranslationValue, float maxTranslationValue, float maxInputValue)
    {

        //Converts a potentionmeter value to a percentage step between a min and a max value.
        float range = maxTranslationValue - minTranslationValue;

        float percentageValue = maxInputValue / range;
        float translation = input / percentageValue;

        float newTranslation = minTranslationValue + translation;

        return newTranslation;

    }

    //This called when the application quits (in editor and in standalone)
    void OnApplicationQuit()
    {
        //We must close the serial port
        serialPort.Close();
    }
}