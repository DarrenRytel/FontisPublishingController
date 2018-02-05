using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float drag = 0.5f;
    public float terminalRoationSpeed = 25.0f;
    public Vector3 MoveVector { set; get; }
    public VitrualJoystick joyStick { set; get; }

    private Rigidbody thisRigidbody;
    public Transform camTransform;

	// Use this for initialization
	void Start ()
    {
        thisRigidbody = gameObject.AddComponent<Rigidbody>();
        thisRigidbody.maxAngularVelocity = terminalRoationSpeed;
        thisRigidbody.drag = drag;

        joyStick = GameObject.FindGameObjectWithTag("JoyStick").GetComponent<VitrualJoystick>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        MoveVector = MoveInput();

        MoveVector = RotateWithView();

        Move();
	}
    
    private void Move()
    {
        thisRigidbody.AddForce((MoveVector * moveSpeed));
    }

    private Vector3 MoveInput()
    {
        Vector3 dir = Vector3.zero;

        dir.x = joyStick.Horizontal();
        dir.z = joyStick.Vertical();

        if (dir.magnitude > 1)
            dir.Normalize();

        return dir;
    }

    private Vector3 RotateWithView()
    {
        if (camTransform != null)
        {
            Vector3 dir = camTransform.TransformDirection(MoveVector);
            dir.Set(dir.x, 0, dir.z);
            return dir.normalized * MoveVector.magnitude;
        }
        else
        {
            //if you alread have a camera use this instead
            camTransform = Camera.main.transform;
            //camTransform = GetComponent<PlayerCamera>().CamTransform;
            return MoveVector;
        }
    }
}
