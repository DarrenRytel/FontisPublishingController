using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {
    private const float Y_ANGLE_MIN = 0.0f;
    private const float Y_ANGLE_MAX = 180.0f;

    public VitrualJoystick JoyStick { set; get; }

    private Transform thisTransform;
    private Camera cam;
    public Transform CamTransform { set; get; }

    private float distance = 10.0f;
    private float currentX = 0.0f;
    private float currentY = 0.0f;
    private float sensitivityX = 4.0f;
    private float sensivityY = 1.0f;

    //private Vector3 offset;
    //private float yOffset = 3.5f;
    //public Transform lookAt;

    // Use this for initialization
    void Start ()
    {
        CamTransform = new GameObject("Camera Container").transform;
        cam = CamTransform.gameObject.AddComponent<Camera>();
        cam.tag = "MainCamera";

        thisTransform = transform;

        //offset = new Vector3(0, yOffset, -1f * distance);

	}
	
	// Update is called once per frame
	void Update ()
    {
        currentX += JoyStick.Horizontal() * sensitivityX;
        currentY += JoyStick.Vertical() * sensivityY;

        currentY = ClampAngle(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);
        //transform.position = lookAt.position + offset;
	}

    private void LateUpdate()
    {
        Vector3 dir = new Vector3(0, 0 - distance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        CamTransform.position = thisTransform.position + rotation * dir;
        CamTransform.LookAt(thisTransform.position);
    }

    private float ClampAngle(float angle, float min, float max)
    {
        do
        {
            if (angle < -360)
                angle += 360;
            if (angle > 360)
                angle -= 360;
        }
        while (angle < -360 || angle > 360);
        return Mathf.Clamp(angle, min, max);
    }

    //public void SlideCamera(bool left)
    //{
    //    if (left)
    //        offset = Quaternion.Euler(0, 90, 0) * offset;
    //}
}
