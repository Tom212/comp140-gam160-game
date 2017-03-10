using UnityEngine;
using System.Collections;

public class TurretFire : MonoBehaviour {

    public float reloadTime; //Time delay between when shots can be made.
    public float laserOnTime; //Time that the laser stays enabled during a flash.
    public float laserOffTime; //Time that a laser remains disabled while flashing.
    public GameObject aimpoint; //Object which is the target point of the laser.

    public GameObject smokeEffect;
    public GameObject muzzleEffect;

    private Transform muzzle;
    private Animator animController;
    private LineRenderer laser;

    private bool loaded = true;
    private bool laserFlashing = false;

	// Use this for initialization
	void Start ()
    {

        Cursor.visible = false;
        
        //Stores the transform of the end of the barrel where projectiles will exit from.
        muzzle = GameObject.FindGameObjectWithTag("Muzzle").transform; 

        if (muzzle == null)
        {
            Debug.Log("Gameobject with tag 'Muzzle' not found, ensure the muzzle at the end of the barrel is tagged 'Muzzle'.");
            Debug.Break();
        }

        //Reference to the animatorcontroller for the turret.
        animController = GetComponent<Animator>();

        laser = muzzle.GetComponent<LineRenderer>();

    }

    void Update()
    {
        //Checks for correct input and fires if weapon has reloaded.
        if (Input.GetButtonDown("Fire1") & loaded)
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

    void Fire()
    {

        animController.SetTrigger("Firing");
        StartCoroutine("Reload");

        GameObject mEffect = Instantiate(muzzleEffect, muzzle.parent.transform.position, Quaternion.identity) as GameObject;
        GameObject sEffect = Instantiate(smokeEffect, muzzle.parent.transform.position, Quaternion.identity) as GameObject;
        mEffect.transform.SetParent(muzzle.parent);
        sEffect.transform.SetParent(muzzle.parent);

        //laser.SetPosition(0, muzzle.transform.position);

        RaycastHit hit;
        if (Physics.Raycast(muzzle.transform.position, muzzle.transform.forward, out hit, 1000f))
        {
            //Debug.Log("Hit!");
            ////laser.SetPosition(1, hit.point);
            //StartCoroutine("EnableLaser");
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
        yield return new WaitForSeconds(reloadTime);
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

    //IEnumerator EnableLaser()
    //{
    //    laser.enabled = true;
    //    yield return new WaitForSeconds(laserOnTime);
    //    laser.enabled = false;
    //}

}
