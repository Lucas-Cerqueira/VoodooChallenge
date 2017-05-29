using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Ball") {
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
		if (other.gameObject.tag == "Block") {
			other.gameObject.SetActive (false);
		}
	}
}
