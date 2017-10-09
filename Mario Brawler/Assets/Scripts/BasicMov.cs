using UnityEngine;
using System.Collections;
using System.IO;

public class BasicMov : MonoBehaviour {

	private float inputDirection;
	public float verticalVelocity;
	private bool secondJumpAvailable = false;

	public float jumpSpeed = 10;
	public float speed = 5.0f;
	public float gravity = 1.0f;
	private bool falling;
	public bool JT;

	private Vector3 moveVector;
	private CharacterController controller;


	void Start () {
		controller = GetComponent<CharacterController> ();
	}

	IEnumerator timer()
	{
		verticalVelocity -= gravity;
		JT = true;
		yield return new WaitForSeconds(0.15f);
		JT = false;
	}
		
	void Update(){
		if(Input.GetKeyDown (KeyCode.LeftShift)){
			StartCoroutine (timer());
			if (!IsControllerGrounded ())
				JT = false;
		}
		ofWorld ();
		disableColl ();
		IsControllerGrounded ();
		inputDirection = Input.GetAxis ("Horizontal") * speed;

		if(JT)
			verticalVelocity -= gravity;

		if (IsControllerGrounded () && !JT) {
			print ("stopped");
			verticalVelocity = 0;
		}

		if(IsControllerGrounded()){
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

	public bool IsControllerGrounded(){
		Vector3 leftRayStart;
		Vector3 rightRayStart;

		leftRayStart = controller.bounds.center;
		leftRayStart.y -= 0.5f;
		rightRayStart = controller.bounds.center;
		rightRayStart.y -= 0.5f;

		leftRayStart.x -= controller.bounds.extents.x;
		rightRayStart.x += controller.bounds.extents.x;


		Debug.DrawRay (leftRayStart, Vector3.down/2, Color.red);
		Debug.DrawRay (rightRayStart, Vector3.down/2, Color.green);
		if (falling) {
			if (Physics.Raycast (leftRayStart, Vector3.down, (controller.height / 4) + 0.3f))
				return true;
			if (Physics.Raycast (rightRayStart, Vector3.down, (controller.height / 4) + 0.3f))
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