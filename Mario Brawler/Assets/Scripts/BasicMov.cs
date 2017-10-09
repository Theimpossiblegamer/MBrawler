using UnityEngine;
using System.Collections;
using System.IO;

public class BasicMov : MonoBehaviour {

	private float inputDirection;
	private float verticalVelocity;
	private bool secondJumpAvailable = false;

	public GameObject[] List;
	public float jumpSpeed = 10;
	public float speed = 5.0f;
	public float gravity = 1.0f;
	private bool falling;

	private Vector3 moveVector;
	private CharacterController controller;


	void Start () {
		controller = GetComponent<CharacterController> ();
	}

	void Update(){
		ofWorld ();
		disableColl ();
		IsControllerGrounded ();
		inputDirection = Input.GetAxis ("Horizontal") * speed;

		if(IsControllerGrounded()){
			verticalVelocity = 0;
			if(Input.GetKeyDown(KeyCode.Space)){
				verticalVelocity = jumpSpeed;
				secondJumpAvailable = true;
			}
				
		} else {
			if(Input.GetKeyDown(KeyCode.Space)){
				if(secondJumpAvailable){
					verticalVelocity = jumpSpeed;
					secondJumpAvailable = false;
				}
			}
			verticalVelocity -= gravity;
		}

		moveVector = new Vector3 (inputDirection, verticalVelocity, 0);
		controller.Move (moveVector * Time.deltaTime);
	}

	private bool IsControllerGrounded(){
		Vector3 leftRayStart;
		Vector3 rightRayStart;

		leftRayStart = controller.bounds.center;
		rightRayStart = controller.bounds.center;

		leftRayStart.x -= controller.bounds.extents.x;
		rightRayStart.x += controller.bounds.extents.x;


		Debug.DrawRay (leftRayStart, Vector3.down, Color.red);
		Debug.DrawRay (rightRayStart, Vector3.down, Color.green);
		if (falling) {
			if (Physics.Raycast (leftRayStart, Vector3.down, (controller.height / 2) + 0.2f))
				return true;
			if (Physics.Raycast (rightRayStart, Vector3.down, (controller.height / 2) + 0.2f))
				return true;
		}
		return false;
	}

	void disableColl(){
		if(verticalVelocity <=0){
			falling = true;
		}
		if(verticalVelocity >0)
		{
			falling = false;
		}
	}
	void ofWorld(){
		if (this.transform.position.y < -7)
			this.transform.position = new Vector3 (0f, 2f, -1.5f);
	}
}