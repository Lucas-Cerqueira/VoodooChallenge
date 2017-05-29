using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitChange : MonoBehaviour {
	private bool isMoving;
	private Vector2 target;
	private bool test = true;

	public float distance = 500.0f;
	public float speed = 1.0f;

	// Use this for initialization
	void Start () {
		isMoving = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (isMoving) {
			Camera.main.transform.Translate (Vector3.up * speed);
			if (Vector2.Distance (new Vector2 (Camera.main.transform.position.x, Camera.main.transform.position.y), target) < 1.0f)
				isMoving = false;
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Ball") {
			isMoving = true;
			target = new Vector2 (Camera.main.transform.position.x, Camera.main.transform.position.y);
			target += Vector2.up;
			target *= distance;
		}
	}
}
