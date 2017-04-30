using UnityEngine;
using System.Collections;

public class TurretFire : MonoBehaviour {

    public float reloadTime; //Time delay between when shots can be made.
    public float laserOnTime; //Time that the laser stays enabled during a flash.
    public float laserOffTime; //Time that a laser remains disabled while flashing.
    public GameObject aimpoint; //Object which is the target point of the laser.
    public float fireEnabledDelay = 1f;

    public bool LEDEnabled = false;

    public GameObject smokeEffect;
    public GameObject muzzleEffect;

    private Transform muzzle;
    private Animator animController;
    private LineRenderer laser;
    private ArduinoControllerInput arduinoController;

    private bool controllerSetup;
    private int lastFireSwitchValue;
    private bool loaded = true;
    private bool laserFlashing = false;

    private TurretAudio audioControl;

	// Use this for initialization
	void Start ()
    {

        Cursor.visible = false;

        audioControl = GetComponent<TurretAudio>();

        //Stores the transform of the end of the barrel where projectiles will exit from.
        muzzle = GameObject.FindGameObjectWithTag("Muzzle").transform; 

        if (muzzle == null)
        {
            Debug.Log("Gameobject with tag 'Muzzle' not found, ensure the muzzle at the end of the barrel is tagged 'Muzzle'.");
            Debug.Break();
        }

        //Reference to the animatorcontroller for the turret.
        animController = GetComponent<Animator>();
        arduinoController = GetComponent<ArduinoControllerInput>();
        laser = muzzle.GetComponent<LineRenderer>();

        controllerSetup = false;

    }

    void Update()
    {
        //Checks for correct input and fires if weapon has reloaded.

        //Allows testing of loading lights.
        if (Input.GetKeyDown(KeyCode.F2))
        {
            LEDEnabled = !LEDEnabled;
            Debug.Log("LED's Enabled = " + LEDEnabled);
        }
        

        if (Firing() & loaded & Time.time > fireEnabledDelay & !audioControl.PlayingSound())
        {
            Fire();
        }


        //Runs a routine that flashes the laser on and off, after this it will repeat.
        if (!laserFlashing)
        {
            StartCoroutine("FlashLaser");
        }

        //Updates the laser position if it's currently on.
        if (laser.enabled == true)
        {
            laser.SetPosition(0, muzzle.transform.position);
            laser.SetPosition(1, aimpoint.transform.position);
        }
    }

    bool Firing ()
    {
        bool fired = false;

        int currentFireSwitchValue = arduinoController.GetFireSwitchInput();

        //Checks that the controller has had time to send over the starting switch settings.
        if (controllerSetup)
        {
            
            if (currentFireSwitchValue != lastFireSwitchValue)
            {
                //Checks to see if the switch has changed value.
                lastFireSwitchValue = currentFireSwitchValue;
                fired = true;

            }
            else
            {
                fired = false;
            }
        }
        else
        {
            lastFireSwitchValue = currentFireSwitchValue;
            fired = false;
            controllerSetup = true;
        }

        

        return fired;


    }

    void Fire()
    {

        animController.SetTrigger("Firing");
        audioControl.FireSound();
        StartCoroutine("Reload");

        GameObject mEffect = Instantiate(muzzleEffect, muzzle.parent.transform.position, Quaternion.identity) as GameObject;
        GameObject sEffect = Instantiate(smokeEffect, muzzle.parent.transform.position, Quaternion.identity) as GameObject;
        mEffect.transform.SetParent(muzzle.parent);
        sEffect.transform.SetParent(muzzle.parent);

        RaycastHit hit;
        if (Physics.Raycast(muzzle.transform.position, muzzle.transform.forward, out hit, 1000f))
        {

            GameObject target = hit.transform.gameObject;

            if (target.CompareTag("Tank"))
            {
                TankAI enemyControl = target.GetComponent<TankAI>();
                enemyControl.Die();
            }

        }


    }

    IEnumerator Reload()
    {
        
        loaded = false;
        if (LEDEnabled)
        {
            arduinoController.SendData("loading");
        }
        yield return new WaitForSeconds(reloadTime);
        if (LEDEnabled)
        {
            arduinoController.SendData("loaded");
        }
        audioControl.ReloadSound();
        loaded = true;
    }

    IEnumerator FlashLaser()
    {

        laserFlashing = true;
        laser.enabled = true;
        yield return new WaitForSeconds(laserOnTime);
        laser.enabled = false;
        yield return new WaitForSeconds(laserOffTime);
        laserFlashing = false;
            
    }

}
