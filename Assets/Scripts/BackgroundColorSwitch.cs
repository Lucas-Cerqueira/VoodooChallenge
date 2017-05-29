using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundColorSwitch : MonoBehaviour {

	public List<Color> colorArray;

	private List<int> layers = new List<int>(3);
	private GameObject ball;
	private SpriteRenderer bgSprite;
	private int currentColor = 0;

	// Use this for initialization
	void Start () 
	{
		bgSprite = GameObject.Find ("Background").GetComponent<SpriteRenderer> ();
		ball = GameObject.Find ("Ball");
		layers.Add(LayerMask.NameToLayer("BlueLayer"));
		layers.Add(LayerMask.NameToLayer("RedLayer"));
		layers.Add(LayerMask.NameToLayer("YellowLayer"));

	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.Space)) 
		{
			currentColor++;
			if (currentColor > colorArray.Count-1)
				currentColor = 0;
			bgSprite.color = colorArray [currentColor];
			ball.layer = layers [currentColor];

		}
	}

	public void chgBlue ()
	{
		currentColor = 0;
		bgSprite.color = colorArray [currentColor];
		ball.layer = layers [currentColor];
	}

	public void chgRed ()
	{
		currentColor = 1;
		bgSprite.color = colorArray [currentColor];
		ball.layer = layers [currentColor];
	}

	public void chgYellow()
	{
		currentColor = 2;
		bgSprite.color = colorArray [currentColor];
		ball.layer = layers [currentColor];
	}
}
