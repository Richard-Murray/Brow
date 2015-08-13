using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Controller2D))]
public class Player : MonoBehaviour {

	public float jumpHeight = 4;
	public float timeToJumpApex = 0.4f;
	float accelerationTimeAirborne = 0.2f;
	float accelerationTimeGrounded = 0.1f;
	float moveSpeed = 6;

	float timeSinceGrounded = 0;

	//float gravity = -20;
	//float jumpVelocity = 8;
	float gravity;
	float jumpVelocity;
	Vector3 velocity;
	float velocityXSmoothing;

	Controller2D controller;

	// Use this for initialization
	void Start () {
		controller = GetComponent<Controller2D>();

		gravity = -(2 * jumpHeight) / Mathf.Pow (timeToJumpApex, 2);
		jumpVelocity = Mathf.Abs (gravity) * timeToJumpApex;
		print ("Gravity: " + gravity + "Jump Velocity: " + jumpVelocity);
	}
	
	// Update is called once per frame
	void Update () {

		if (controller.collisions.above || controller.collisions.below) {
			velocity.y = 0;
		}

		//Get input vector
		Vector2 input = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));


		float targetVelocityX = 0;

		timeSinceGrounded += Time.deltaTime;
		if (controller.collisions.below == true)
			timeSinceGrounded = 0;

		//Take the platform's velocity into account when jumping
		if(Input.GetKeyDown (KeyCode.Space) && (controller.collisions.below || timeSinceGrounded < 0.1f)) {
			velocity.y = jumpVelocity + controller.collisions.platformVelocity.y;
			velocity.x = velocity.x + controller.collisions.platformVelocity.x;
			targetVelocityX = controller.collisions.platformVelocity.x;
		}

		//velocity.x = input.x * moveSpeed;

		//Calculate x velocity
		if (!(input.x < 0 && controller.collisions.left) && !(input.x > 0 && controller.collisions.right)) {
			targetVelocityX += input.x * moveSpeed;
			velocity.x = Mathf.SmoothDamp (velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
		}

		//Zero velocities if there is a collision on that axis
		if ((controller.collisions.left && velocity.x < 0) || (controller.collisions.right && velocity.x > 0)) 
			velocity.x = 0;
		if (controller.collisions.above)
			velocity.y = 0;

			//velocity.x = velocity.x + controller.collisions.platformVelocity.x;

		//Send move command through to the controller2D
		velocity.y += gravity * Time.deltaTime;
		controller.Move(velocity * Time.deltaTime);
	}
}
