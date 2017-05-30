using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

public class BlockSpawner : MonoBehaviour {

	//public GameObject scoreCounterPrefab;
	public float screenWidthInWorld = 5.6f;
	public int amountOfLinesToKeepSpawnedAboveScreen = 3;   //Sorry for the long name, feel free to change to something better
	public float stdDistanceY; //Gaussian std for the Y distance distribution between the blocks
	public float stdDistanceX; //Gaussian std for the X distance distribution between the blocks
	public float stdNumberOfBlocks; // Gaussian std for the number of blocks spawned per line
	public float stdColorOfBlocks;
	public int numberOfXRepetitions = 2;
	public int coolOfXRepetitions = 5;

	private float lastTopLinePositionY;
	private float distanceBetweenLines;
	private List<Transform> verticalPositions = new List<Transform>();
	private Pool blocksPool;
	private Pool scoreTriggerPool;
	private float blockWidth;
	private List<Color> colorArray;
	private int currentTopLinesSpawned = 0;
	private int[] colorCount = new int[3];
	private int[] positionCount = new int[3];
	private int lastLineCount;
	private bool lastLineBlackBlock;



	// Use this for initialization
	void Start () 
	{
		lastLineBlackBlock = false;
		lastLineCount = 0;
		for (int i = 0; i < colorCount.GetLength (0); i++) {
			colorCount [i] = 0;
			positionCount [i] = 0;
		}
		colorArray = GameObject.Find ("Background").GetComponent<BackgroundColorSwitch> ().colorArray;
		GameObject.Find ("SpawnPositions").GetComponentsInChildren<Transform> (verticalPositions);
		verticalPositions.RemoveAt (0);
		distanceBetweenLines = Mathf.Abs (verticalPositions [2].position.y - verticalPositions [1].position.y); // Distance between middle and top line
		lastTopLinePositionY = verticalPositions[2].position.y;

		Pool[] pools = GetComponents<Pool> ();
		blocksPool = pools [0];
		scoreTriggerPool = pools [1];

		int n = (int)GaussianDistribution.NextGaussianDouble (stdNumberOfBlocks, 0, 0, 3) + 1; //Random.Range (2, 4);
		SpawnLineOfBlocks (n, verticalPositions [0].position.y);  	// Spawn the bottom line
		n = (int) GaussianDistribution.NextGaussianDouble (stdNumberOfBlocks, 0, 0, 3) + 1;//Random.Range (2, 4);
		SpawnLineOfBlocks (n, verticalPositions [1].position.y);	// Spawn the middle line

		for (int i = 0; i < amountOfLinesToKeepSpawnedAboveScreen; i++) 
		{
			n = (int) GaussianDistribution.NextGaussianDouble (stdNumberOfBlocks, 0, 0, 3) + 1;//Random.Range (2, 4);
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
		int n = (int) GaussianDistribution.NextGaussianDouble (stdNumberOfBlocks, 0, 0, 3) + 1;//Random.Range (2, 4);
		SpawnLineOfBlocks (n, lastTopLinePositionY);	// Spawn one top lane
		lastTopLinePositionY += distanceBetweenLines;
	}



//	void SpawnLineOfBlocks (int amount, float yPosition)
//	{
//		int colorIndex = 0;
//		int blackBlocksCounter = 0;
//		Color color;
//		float blockWidth = screenWidthInWorld / (float) amount;
//
//		List<GameObject> blocks = new List<GameObject> ();
//		float firstPosition;
//		if (amount == 2)
//			firstPosition = -screenWidthInWorld / 2.0f + blockWidth / 2.0f;  // Screen left limit + gap to keep blocks centered + half of blockWidth
//		else // if n==3
//			firstPosition = -screenWidthInWorld / 2.0f + blockWidth / 2.0f; // Screen left limit + half of blockWidth
//
//
//		GameObject scoreTrigger = scoreTriggerPool.Instantiate ();
////		if (scoreTrigger)
//			scoreTrigger.transform.position = new Vector3 (0, yPosition, 0);
////		else
////			print ("Nao spawnou trigger");
//
//
//		for (int i = 0; i < amount; i++) 
//		{
//			// colorIndex == colorArray.Count -> BLACK color
//			do 
//			{
//				colorIndex = Random.Range (0, colorArray.Count + 1);
//			} while ((colorIndex == colorArray.Count && blackBlocksCounter >= amount-1) || (amount==3 && i==1 && colorIndex == colorArray.Count));
//
//			if (colorIndex == colorArray.Count) 
//			{
//				color = Color.black;
//				blackBlocksCounter++;
//			}
//			else
//				color = colorArray [colorIndex];
//			
//			float xPosition = firstPosition + i * screenWidthInWorld / (float)amount;
//			GameObject obj = blocksPool.Instantiate ();
//			blocks.Add (obj);
//			obj.GetComponent<SpriteRenderer> ().color = color;
//			obj.transform.localScale = new Vector3 (blockWidth * 100, obj.transform.localScale.y, 1);
//			obj.layer = colorIndex + LayerMask.NameToLayer ("RedLayer");
//			blocks [i].transform.position = new Vector3 (xPosition, yPosition, 0);
//
//		}
//	}

	void SpawnLineOfBlocks (int amount, float yPosition)
	{
		int colorIndex = 0;
		int blackBlocksCounter = 0;
		int[] tempColorCount = new int[3];
		Color color;
		float blockWidth = screenWidthInWorld / (float) 3;

		List<GameObject> blocks = new List<GameObject> ();
		float firstPosition;
		firstPosition = -screenWidthInWorld / 2.0f + blockWidth / 2.0f; // Screen left limit + half of blockWidth


		GameObject scoreTrigger = scoreTriggerPool.Instantiate ();
		//		if (scoreTrigger)
		scoreTrigger.transform.position = new Vector3 (0, yPosition, 0);
		//		else
		//			print ("Nao spawnou trigger");


		List <bool> used = new List <bool>();
		for (int i = 0; i < 3; i++)
			used.Add(false);

		for (int i = 0; i < amount; i++) 
		{
			// colorIndex == colorArray.Count -> BLACK color
//			do 
//			{
//				colorIndex = Random.Range (0, colorArray.Count + 1);
//			} while ((colorIndex == colorArray.Count && blackBlocksCounter >= amount-1) || (amount==3 && i==1 && colorIndex == colorArray.Count));

			float colorMax = Array.IndexOf(colorCount, colorCount.Max());
			float colorMin = Array.IndexOf(colorCount, colorCount.Min());
			print ("colorMax: " + colorMax + " ColorMin: " + colorMin);

			if (lastLineCount < 2 || lastLineBlackBlock) {
				int tempIndex =UnityEngine.Random.Range(0,3);
				tempIndex = tempIndex==colorMax?(int)colorMin:tempIndex;
				//colorIndex = (int)GaussianDistribution.NextGaussianDouble (stdColorOfBlocks, colorMean, 0, 2);
				colorIndex = tempIndex;
				print ("index: " + tempIndex);
			}
			else if (UnityEngine.Random.Range(0,3) == 0)
				colorIndex = (int) GaussianDistribution.NextGaussianDouble (stdColorOfBlocks, 3, 0, 3);
			else
			{
				int tempIndex =UnityEngine.Random.Range(0,3);
				tempIndex = tempIndex==colorMax?(int)colorMin:tempIndex;
				//colorIndex = (int)GaussianDistribution.NextGaussianDouble (stdColorOfBlocks, colorMean, 0, 2);
				colorIndex = tempIndex;
				print ("index: " + tempIndex);
			}

				
			if (colorIndex == colorArray.Count) {
				color = Color.black;
				blackBlocksCounter++;
				lastLineBlackBlock = true;
			} else {
				color = colorArray [colorIndex];
				tempColorCount [colorIndex]++;
				colorCount [colorIndex]++;
			}

			int position;
			int [] result = Enumerable.Range (0, used.Count).Where (j => used [j] == false).ToArray ();
			int index = UnityEngine.Random.Range (0, result.GetLength (0));
			position = result [index];
			positionCount [position]++;

			if (positionCount [position] >= numberOfXRepetitions) {
				if (positionCount[position] > coolOfXRepetitions)
					positionCount [position] = 0;
				if (result.GetLength (0) == 1)
					break;
				else {
					position = result [result.GetLength (0) - index - 1];
				}
			}
			used [position] = true;




			float xPosition = firstPosition + position * screenWidthInWorld / (float)3;

			if (position ==1)
				xPosition += (float) GaussianDistribution.NextGaussianDouble (stdDistanceX, 0, -0.3f, 0.3f);
			if (position == 0) 
				xPosition += (float)GaussianDistribution.NextGaussianDouble (stdDistanceX, 0, 0, 0.3f);
			if (position == 2)
				xPosition += (float) GaussianDistribution.NextGaussianDouble (stdDistanceX, 0, -0.3f, 0);

			yPosition += (float) GaussianDistribution.NextGaussianDouble (stdDistanceX, 0, -0.75f, 0.75f);
			
			GameObject obj = blocksPool.Instantiate ();
			blocks.Add (obj);
			obj.GetComponent<SpriteRenderer> ().color = color;
			obj.transform.localScale = new Vector3 (blockWidth * 100, obj.transform.localScale.y, 1);
			obj.layer = colorIndex + LayerMask.NameToLayer ("RedLayer");
			blocks [i].transform.position = new Vector3 (xPosition, yPosition, 0);

		}

		for (int i = 0; i < colorCount.GetLength (0); i++)
			colorCount [i] = tempColorCount[i];
		
		lastLineCount = amount;

		if (blackBlocksCounter == 0)
			lastLineBlackBlock = false;
	}
}
