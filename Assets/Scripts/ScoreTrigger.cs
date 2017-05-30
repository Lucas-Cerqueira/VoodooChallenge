using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTrigger : MonoBehaviour {
	
	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.CompareTag ("Ball") && other.GetComponent<BallMovement>().GetGoingUp()) 
		{
			ScoreHandler.IncreaseScore ();
		}
	}
}
