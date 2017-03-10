using UnityEngine;
using System.Collections;

public class ViewController : MonoBehaviour {

    public enum SelectedView
    {
        overview,
        gunview
    }

    public float cameraRotateSpeed;
    public float maxCameraRotateSpeed;

    private GameObject overviewCamera;
    private GameObject gunviewCamera;
    private Rigidbody overviewCenterRb;
    private SelectedView currentView;


	// Use this for initialization
	void Start () {

        overviewCamera = GameObject.FindGameObjectWithTag("OverviewCamera");
        gunviewCamera = GameObject.FindGameObjectWithTag("GunviewCamera");
        gunviewCamera.SetActive(false);

        overviewCenterRb = overviewCamera.GetComponentInParent<Rigidbody>();
        overviewCenterRb.maxAngularVelocity = maxCameraRotateSpeed; //Caps rotation speed.

        currentView = SelectedView.overview;
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetButtonDown("Overview") & currentView != SelectedView.overview)
        {
           
            ChangeView(SelectedView.gunview, SelectedView.overview);
            currentView = SelectedView.overview;
        }

        if(Input.GetButtonDown("Gunview") & currentView != SelectedView.gunview)
        {

            ChangeView(SelectedView.overview, SelectedView.gunview);
            currentView = SelectedView.gunview;
        }

    }

    void FixedUpdate()
    {
        if (currentView == SelectedView.overview)
        {
            Vector3 cameraRotate = OverviewRotation();
            overviewCenterRb.AddRelativeTorque(cameraRotate);
        }

    }


    Vector3 OverviewRotation()
    {

        float horizontalMovement = Input.GetAxis("CameraRotate");
        horizontalMovement *= cameraRotateSpeed; //Applies movement multiplier.
        Vector3 rotationTorque = new Vector3(0f, horizontalMovement, 0f);

        return rotationTorque;

    }

    void ChangeView (SelectedView currentCam, SelectedView nextCam)
    {

        SetCamera(currentCam, false);
        SetCamera(nextCam, true);

    }

    void SetCamera(SelectedView camera, bool enabled)
    {

        switch (camera)
        {

            case SelectedView.overview:
                overviewCamera.SetActive(enabled);
                break;
            case SelectedView.gunview:
                gunviewCamera.SetActive(enabled);
                break;

        }

    }

}
