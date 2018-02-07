using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPivot : MonoBehaviour {

    [SerializeField]
    private float rotateSpeed = 3f;

    private VitrualJoystick cameraJoystick;

    [SerializeField]
    private Transform camHold;

    [SerializeField]
    private Transform target;

    private void Awake()
    {
        cameraJoystick = GameObject.FindGameObjectWithTag("CameraJoyStick").GetComponent<VitrualJoystick>();

        if (!target)
        {
            Debug.LogError("No target.");
        }

        if (!cameraJoystick)
        {
            Debug.LogError("Joystick camera is null.");
        }

        if (!camHold)
        {
            Debug.LogError("Cam hold is null.");
        }
    }

    void Update()
    {
        transform.position = new Vector3(target.position.x, transform.position.y, target.position.z);
        transform.Rotate(-(MoveInput() * rotateSpeed));
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 0);
    }

    private Vector3 MoveInput()
    {
        Vector3 dir = Vector3.zero;

        dir.y = cameraJoystick.Horizontal();
        dir.x = -cameraJoystick.Vertical();

        if (dir.magnitude > 1)
            dir.Normalize();

        return dir;
    }
}

