using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public float followOffset = 3f;

	private bool follow = false;
	private BallMovement ballMovementScript;
	private Transform ballTransform;
	private Vector3 relativePositionToBall;
	private float initialPositionX;
	private bool previousBallGoingUp = false;

	void OnTriggerEnter2D(Collider2D other) 
	{
		if (other.gameObject.tag == "Ball") 
		{
			follow = true;
			relativePositionToBall = transform.position - ballTransform.position;
		}
	}

	void OnTriggerExit2D(Collider2D other) 
	{
		if (other.gameObject.tag == "Ball") 
		{
			follow = false;
		}
	}

	// Use this for initialization
	void Start () 
	{
		GameObject ball = GameObject.Find ("Ball");
		ballMovementScript = ball.GetComponent<BallMovement> ();
		ballTransform = ball.transform;
		relativePositionToBall = transform.position - ballTransform.position;
		initialPositionX = transform.position.x;
	}
	
	// Update is called once per frame
	void Update () 
	{
		bool goingUp = ballMovementScript.GetGoingUp ();
		if (goingUp && follow) {
			transform.position = (ballTransform.position + relativePositionToBall);
			transform.position = new Vector3 (initialPositionX, transform.position.y, transform.position.z);
		}
	}
}
