using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCollision : MonoBehaviour {

	public enum Side
	{
		left=1, right=-1
	}

	public Side side = Side.left;


	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.CompareTag ("Ball")) 
		{
			Rigidbody2D rb = other.gameObject.GetComponent<Rigidbody2D> ();
			rb.velocity = new Vector2 (Mathf.Abs(rb.velocity.x) * (int)side, rb.velocity.y);
		}
	}

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
