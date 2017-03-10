using UnityEngine;
using System.Collections;

public class TurretMovement : MonoBehaviour {

    public float slewForce; //Force at which turret rotates.
    public float maxSlewSpeed; //Fastest speed that the turret can turn at.

    public float elevationForce; //Force at which gun elevates.
    public float maxElevationSpeed; //Maximum speed elevation can move at.

    private Rigidbody headRb; //Rigidbody attached to the head of the turret which rotates.
    private Rigidbody barrelRb; //Rigidbody attached to the barrel housing which elevates.

    private GameObject barrelHinge; //Gameobject which holds the barrelRb and hinge.

	void Start ()
    {

        //Finds the head object and stores its rigidbody.
        GameObject turretHead = transform.FindChild("Head").gameObject;
        headRb = turretHead.GetComponent<Rigidbody>();
        headRb.maxAngularVelocity = maxSlewSpeed; //Caps rotation speed.

        //Finds barrel housing gameobject and stores its rigidbody.
        barrelHinge = turretHead.transform.FindChild("BarrelHinge").gameObject;
        barrelRb = barrelHinge.GetComponent<Rigidbody>();
        barrelRb.maxAngularVelocity = maxElevationSpeed; //Caps elevation speed.

	}
	


    void FixedUpdate ()
    {
        //Gets slew input and applies it to head rigidbody.
        Vector3 slew = SlewInput();
        headRb.AddRelativeTorque(slew);

        //Gets elevation input and applies it to the barrel housing rigidbody.
        Vector3 elevation = ElevationInput();
        barrelRb.AddRelativeTorque(elevation);

    }

    Vector3 SlewInput()
    {

        float horizontalMovement = Input.GetAxis("Horizontal");
        horizontalMovement *= slewForce; //Applies movement multiplier.
        Vector3 rotationTorque = new Vector3(0f, horizontalMovement, 0f);

        return rotationTorque;

    }

    Vector3 ElevationInput()
    {

        float verticalMovement = Input.GetAxis("Vertical");
        verticalMovement *= elevationForce; //Applies movement multiplier.
        Vector3 elevationTorque = new Vector3(0f, 0f, verticalMovement);

        return elevationTorque;
    }

}
