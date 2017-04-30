int horizontalSensorPin = A0;   
int horizontalSensorValue = 0;  
int verticalSensorPin = A1;
int verticalSensorValue = 0;
int zoomSensorPin = A2;
int zoomSensorValue = 0;

int fireSwitchSensorPin = 3;
int fireSwitchSensorValue;
int viewSwitchSensorPin = 2;
int viewSwitchSensorValue;

int loadingLEDPin = 8;
int readyLEDPin = 4;


void setup() {
  // put your setup code here, to run once:
  Serial.begin(9600);
  //Initialize firing/view switch pin.
  pinMode(fireSwitchSensorPin,INPUT_PULLUP);
  pinMode(viewSwitchSensorPin,INPUT_PULLUP);
  //Setup LED's.
  pinMode(loadingLEDPin, OUTPUT);
  pinMode(readyLEDPin, OUTPUT);
  
  
}

void loop() {
  // put your main code here, to run repeatedly:

  

  horizontalSensorValue = analogRead(horizontalSensorPin);
  verticalSensorValue = analogRead(verticalSensorPin);

  zoomSensorValue = analogRead(zoomSensorPin);

  fireSwitchSensorValue = digitalRead(fireSwitchSensorPin);
  viewSwitchSensorValue = digitalRead(viewSwitchSensorPin);
  
  //Compile all input into a string to send to game.
  String sensorData = String(horizontalSensorValue) + "," + String(verticalSensorValue) + "," + String(fireSwitchSensorValue) + "," + String(viewSwitchSensorValue) + "," + String(zoomSensorValue);

  Serial.println(sensorData);
  delay(50);

}

void serialEvent() { //Called when written to.
  
    String inputData = Serial.readString();

    //Checks input to decide how to manage LED's.
    if (inputData == "loading") 
    {
      ToggleLight(1, 0);
      ToggleLight (0, 1);
    } else {
      ToggleLight(1, 1);
      ToggleLight (0, 0);
    }
    
  }

void ToggleLight (int lightNo, int state) {
        
      //Light assigns a number to the LED's, takes an input to set them on/off.
      
      int lightPort;

      if (lightNo == 0) 
      {
        lightPort = loadingLEDPin;
      } else if (lightNo == 1)
      {
        lightPort = readyLEDPin;
      }

      if (state == 0) {
        digitalWrite(lightPort, LOW);  
      } else if (state == 1) {
        digitalWrite(lightPort, HIGH);
      }

 
  }

