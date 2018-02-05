using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {
	public GameObject SupCam;
	public Animator animator;
	public string NameVarRun,  NameVarWalkForward,  NameVarWalkBack,  NameVarStrafeLeft,  NameVarStrafeRight,  NameVarJump, NameVarLooking;
	public float SpeedMove, SpeedRotation;
	public int Run, Jump, Look;
	public float MoveForwarBack, MoveRightLeft; 
	private float tim;

    //finding where the player has started touching the screen
    private Vector2 touchOrigin = -Vector2.one;

	public bool ContactingRope = false;
	// Use this for initialization

	public GameObject myMap;
	//Map Image Current
	public GameObject myLight;
	//Flashlight

	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		if(!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.Space))
		{
			tim += Time.deltaTime;
			if(tim > 5)
				Look = 2;
			if(tim > 8.5f)
			{
				Look = 0;
				tim = 0;
			}
		}
		else
		{
			Look = 0;
			tim = 0;
		}

		if(Input.GetKey(KeyCode.Space))
			Jump = 2;
		else
			Jump = 0;

		if(Input.GetKey(KeyCode.LeftShift))
			Run = 2;
		else
			Run = 0;

		MoveForwarBack = Input.GetAxis("Vertical");
		if(!Input.GetKey(KeyCode.W))
			if(MoveForwarBack == 0)
		{
			animator.SetFloat(NameVarWalkForward, 0);
		}
		if(!Input.GetKey(KeyCode.S))
			if(MoveForwarBack == 0)
		{
			animator.SetFloat(NameVarWalkBack, 0);
		}
		//////////////////////////////////////////////////
		MoveRightLeft = Input.GetAxis("Horizontal");
		if(!Input.GetKey(KeyCode.A))
			if(MoveRightLeft == 0)
		{
			animator.SetFloat(NameVarStrafeLeft, 0);
		}
		if(!Input.GetKey(KeyCode.D))
			if(MoveRightLeft == 0)
		{
			animator.SetFloat(NameVarStrafeRight, 0);
		}
		////////////////////////////////////////////////
		if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
		{
		    transform.position += transform.forward * SpeedMove * MoveForwarBack * Time.deltaTime; 
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(transform.eulerAngles.x, SupCam.transform.eulerAngles.y, transform.eulerAngles.z), 1.5f*Time.deltaTime);
			//transform.rotation = Quaternion.LookRotation(new Vector3(transform.eulerAngles.x, camer.transform.eulerAngles.y, transform.eulerAngles.z));
		}
		if(Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
		{
			transform.position += transform.forward * SpeedMove * MoveForwarBack/3 * Time.deltaTime; 
			transform.position += transform.right * SpeedMove * MoveRightLeft/3 * Time.deltaTime; 
		}
		if(Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
		{
			transform.position += transform.forward * SpeedMove * MoveForwarBack/3 * Time.deltaTime; 
			transform.position += transform.right * SpeedMove * MoveRightLeft/3 * Time.deltaTime; 
		}
		if(Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift))
		{
			transform.position += transform.forward * SpeedMove * MoveForwarBack*2 * Time.deltaTime;  
		}
		if(ContactingRope) {
			SpeedMove = 3f; // use whatever value you want here, just make sure it's larger than below
		} else {
			SpeedMove = 1f;
		}
        /////////////////////////////////////////////////////////
        if (Input.touchCount > 0)
        {
            Touch myTouch = input.touches[0];

            if(myTouch.phase == TouchPhase.Began)
            {
                touchOrigin = myTouch.position;
            }
            else if (myTouch.phase == TouchPhase.Ended && touchOrigin.x >= 0)
            {
                Vector2 touchEnd = myTouch.position;
                float x = touchEnd.x - touchOrigin.x;
                float y = touchEnd.y - touchOrigin.y;
                touchOrigin.x = -1;
                if (Mathf.Abs(x) > Mathf.Abs(y))
                    horizontal = x > 0 ? 1 : -1;
                else
                    vertical = y > 0 ? 1 : -1;
            }
        }
        if (horizontal != 0 || vertical != 0)
            (horizontal, vertical);


    }

	void FixedUpdate () {
		if(Input.GetAxis("Vertical") > 0)
		    animator.SetFloat(NameVarWalkForward, MoveForwarBack);
		if(Input.GetAxis("Vertical") < 0)
			animator.SetFloat(NameVarWalkBack, MoveForwarBack * -1);
		if(Input.GetAxis("Horizontal") > 0)
			animator.SetFloat(NameVarStrafeRight, MoveRightLeft);
		if(Input.GetAxis("Horizontal") < 0)
			animator.SetFloat(NameVarStrafeLeft, MoveRightLeft * -1);

		animator.SetInteger(NameVarRun, Run);
		animator.SetInteger(NameVarJump, Jump);
		animator.SetInteger(NameVarLooking, Look);
	}



	void OnGUI(){

		//if(GUI.Button(new Rect(10, 120, 45, 30), "Map")){
		//myMap.SetActive(true);          
		if (GUI.Button (new Rect(100, 10, 45, 30), "Map"))

			//GameObject myMap = myMapNew;
		{

			if(myMap.active == false)
			{
				myMap.active = true;
			}
			else if(myMap.active == true)
			{
				myMap.active = false;
			}

		}
		if (GUI.Button (new Rect(150, 10, 75, 30), "Flashlight"))

		{
			if(myLight.active == false)
			{
				myLight.active = true;
			}
			else if(myLight.active == true)
			{
				myLight.active = false;
			}

		}


	}

}
