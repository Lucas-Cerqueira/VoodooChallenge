using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreHandler : MonoBehaviour {

	static int score = 0;
	static Text scoreUIText;

	public static void IncreaseScore()
	{
		//print ("Entrou");
		//print ("Score antes: " + score);
		score = score + 1;
		//print ("Score depois: " + score);
		scoreUIText.text = score.ToString ();
	}


	// Use this for initialization
	void Start () 
	{
		scoreUIText = GameObject.Find ("Score").GetComponent<Text> ();
		score = 0;
		scoreUIText.text = score.ToString ();
	}
}
