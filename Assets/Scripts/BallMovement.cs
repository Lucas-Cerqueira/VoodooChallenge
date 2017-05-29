using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour {

	public float force = 100;

	private Rigidbody2D rb;

	void OnCollisionEnter2D (Collision2D other)
	{
		if (other.gameObject.CompareTag("Block") && transform.position.y > other.transform.position.y)
		{
			rb.velocity = Vector2.zero;
		}
	}

	// Use this for initialization
	void Start () 
	{
		rb = GetComponent<Rigidbody2D> ();
		rb.AddForce ((Vector2.up + Vector2.right*0.5f)* force);
	}
	
	// Update is called once per frame
	void Update () 
	{
	}
}
