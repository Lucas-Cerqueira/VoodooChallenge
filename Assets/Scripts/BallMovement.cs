using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour {

	public float force = 100;

	private bool isStarting;
	private bool goingUp = false;
	private Rigidbody2D rb;

	void OnCollisionEnter2D (Collision2D other)
	{
		if (other.gameObject.CompareTag("Block") && transform.position.y > other.transform.position.y +0.3f)
		{
			//rb.velocity = Vector2.zero;
			//isStarting = true;
		}
	}

	public bool GetGoingUp ()
	{
		return goingUp;
	}

	// Use this for initialization
	void Start () 
	{
		isStarting = true;
		rb = GetComponent<Rigidbody2D> ();
		rb.velocity = Vector2.zero;
		//rb.AddForce ((Vector2.up + Vector2.right*0.5f)* force);
	}
	
	// Update is called once per frame
	void Update () 
	{
		goingUp = rb.velocity.y > 0;
		if (isStarting) 
		{
			Vector3 direction = Vector3.zero;
			#if UNITY_STANDALONE
			if (Input.GetMouseButtonUp (0))
			{
				direction = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
				direction.z= 0.0f;
			}
			#endif
			#if UNITY_ANDROID
			if (Input.GetTouch(0).phase == TouchPhase.Ended)
			{
				direction = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x,Input.GetTouch(0).position.y,0.0f));
				direction.z = 0.0f;
			}
			#endif
			if (direction.y > 0) {
				direction.Normalize ();
				rb.AddForce (new Vector2 (direction.x * force, direction.y * force));
				isStarting = false;
			}
		}
	}
}
