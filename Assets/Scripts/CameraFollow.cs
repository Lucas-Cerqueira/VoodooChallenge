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
	private float initialBallPositionY;
	private bool previousBallGoingUp = false;

	// Use this for initialization
	void Start () 
	{
		GameObject ball = GameObject.Find ("Ball");
		ballMovementScript = ball.GetComponent<BallMovement> ();
		ballTransform = ball.transform;
		relativePositionToBall = transform.position - ballTransform.position;
		initialPositionX = transform.position.x;
		initialBallPositionY = ballTransform.position.y;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Mathf.Abs (ballTransform.position.y - initialBallPositionY) >= followOffset && !follow) 
		{
			follow = true;
			relativePositionToBall = transform.position - ballTransform.position;
		}
			

		bool goingUp = ballMovementScript.GetGoingUp ();
		if (goingUp && follow) {
			transform.position = (ballTransform.position + relativePositionToBall);
			transform.position = new Vector3 (initialPositionX, transform.position.y, transform.position.z);
		} else 
		{
			follow = false;
		}

		if (goingUp && !previousBallGoingUp) 
		{
			follow = false;
			initialBallPositionY = ballTransform.position.y;
		}

		previousBallGoingUp = goingUp;
	}
}
