using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour {

	//public GameObject scoreCounterPrefab;
	public float screenWidthInWorld = 5.6f;
	public int amountOfLinesToKeepSpawnedAboveScreen = 3;   //Sorry for the long name, feel free to change to something better

	private float lastTopLinePositionY;
	private float distanceBetweenLines;
	private List<Transform> verticalPositions = new List<Transform>();
	private Pool blocksPool;
	private Pool scoreTriggerPool;
	private float blockWidth;
	private List<Color> colorArray;
	private int currentTopLinesSpawned = 0;



	// Use this for initialization
	void Start () 
	{
		colorArray = GameObject.Find ("Background").GetComponent<BackgroundColorSwitch> ().colorArray;
		GameObject.Find ("SpawnPositions").GetComponentsInChildren<Transform> (verticalPositions);
		verticalPositions.RemoveAt (0);
		distanceBetweenLines = Mathf.Abs (verticalPositions [2].position.y - verticalPositions [1].position.y); // Distance between middle and top line
		lastTopLinePositionY = verticalPositions[2].position.y;

		Pool[] pools = GetComponents<Pool> ();
		blocksPool = pools [0];
		scoreTriggerPool = pools [1];

		int n = Random.Range (2, 4);
		SpawnLineOfBlocks (n, verticalPositions [0].position.y);  	// Spawn the bottom line
		n = Random.Range (2, 4);
		SpawnLineOfBlocks (n, verticalPositions [1].position.y);	// Spawn the middle line

		for (int i = 0; i < amountOfLinesToKeepSpawnedAboveScreen; i++) 
		{
			n = Random.Range (2, 4);
			SpawnLineOfBlocks (n, lastTopLinePositionY);	// Spawn one top lane
			lastTopLinePositionY += distanceBetweenLines;
			currentTopLinesSpawned++;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
		

	public void SpawnNextTopLine ()
	{
		int n = Random.Range (2, 4);
		SpawnLineOfBlocks (n, lastTopLinePositionY);	// Spawn one top lane
		lastTopLinePositionY += distanceBetweenLines;
	}



	void SpawnLineOfBlocks (int amount, float yPosition)
	{
		int colorIndex = 0;
		int blackBlocksCounter = 0;
		Color color;
		float blockWidth = screenWidthInWorld / (float) amount;

		List<GameObject> blocks = new List<GameObject> ();
		float firstPosition;
		if (amount == 2)
			firstPosition = -screenWidthInWorld / 2.0f + blockWidth / 2.0f;  // Screen left limit + gap to keep blocks centered + half of blockWidth
		else // if n==3
			firstPosition = -screenWidthInWorld / 2.0f + blockWidth / 2.0f; // Screen left limit + half of blockWidth


		GameObject scoreTrigger = scoreTriggerPool.Instantiate ();
//		if (scoreTrigger)
			scoreTrigger.transform.position = new Vector3 (0, yPosition, 0);
//		else
//			print ("Nao spawnou trigger");


		for (int i = 0; i < amount; i++) 
		{
			// colorIndex == colorArray.Count -> BLACK color
			do 
			{
				colorIndex = Random.Range (0, colorArray.Count + 1);
			} while ((colorIndex == colorArray.Count && blackBlocksCounter >= amount-1) || (amount==3 && i==1 && colorIndex == colorArray.Count));

			if (colorIndex == colorArray.Count) 
			{
				color = Color.black;
				blackBlocksCounter++;
			}
			else
				color = colorArray [colorIndex];
			
			float xPosition = firstPosition + i * screenWidthInWorld / (float)amount;
			GameObject obj = blocksPool.Instantiate ();
			blocks.Add (obj);
			obj.GetComponent<SpriteRenderer> ().color = color;
			obj.transform.localScale = new Vector3 (blockWidth * 100, obj.transform.localScale.y, 1);
			obj.layer = colorIndex + LayerMask.NameToLayer ("RedLayer");
			blocks [i].transform.position = new Vector3 (xPosition, yPosition, 0);

		}
	}
}
