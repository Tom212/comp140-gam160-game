using UnityEngine;
using System.Collections;

public class TurretMovement : MonoBehaviour {

    public float slewForce; //Force at which turret rotates.
    public float maxSlewSpeed; //Fastest speed that the turret can turn at.

    public float elevationForce; //Force at which gun elevates.
    public float maxElevationSpeed; //Maximum speed elevation can move at.

    private ArduinoControllerInput arduinoController;

    private Vector3 targetElevation;

    private Rigidbody headRb; //Rigidbody attached to the head of the turret which rotates.
    private Rigidbody barrelRb; //Rigidbody attached to the barrel housing which elevates.

    private GameObject barrelHinge; //Gameobject which holds the barrelRb and hinge.

    void Start()
    {

        //Finds the head object and stores its rigidbody.
        GameObject turretHead = transform.FindChild("Head").gameObject;
        headRb = turretHead.GetComponent<Rigidbody>();
        headRb.maxAngularVelocity = maxSlewSpeed; //Caps rotation speed.

        //Finds barrel housing gameobject and stores its rigidbody.
        barrelHinge = turretHead.transform.FindChild("BarrelHinge").gameObject;
        barrelRb = barrelHinge.GetComponent<Rigidbody>();
        barrelRb.maxAngularVelocity = maxElevationSpeed; //Caps elevation speed.

        //Stores reference to the script that receives input from the Arduino controller.
        arduinoController = GetComponent<ArduinoControllerInput>();

    }



    void FixedUpdate()
    {
        //Gets slew input and applies it to head rigidbody.
        Vector3 slew = SlewInput();
        headRb.AddRelativeTorque(slew);

        //Gets elevation input from the controller.
        targetElevation = new Vector3(0f, 0f, arduinoController.GetVerticalInput());

        //Stores the current z rotation of the barrel.
        float currentElevation = barrelRb.transform.localEulerAngles.z;

        if (currentElevation > 180f)
        {
            //If rotation goes into negatives it resets to 360, this creates a absolute number if its larger than 180 degrees to work with.
            currentElevation -= 360f;
        }

        //Offsets force relative to the distance between target and current elevations.
        float newForce = targetElevation.z - currentElevation;
        newForce *= elevationForce;
        barrelRb.AddRelativeTorque(new Vector3(0f, 0f, newForce));


    }

    Vector3 SlewInput()
    {

        float horizontalMovement = arduinoController.GetHorizontalInput();
        horizontalMovement *= slewForce; //Applies movement multiplier.
        Vector3 rotationTorque = new Vector3(0f, horizontalMovement, 0f);

        return rotationTorque;

    }

}
