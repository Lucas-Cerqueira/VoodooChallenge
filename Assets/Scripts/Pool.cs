using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
	private GameObject poolPrefab;
	private int poolSize;
	private bool scalable;

	private List<GameObject> poolList = new List<GameObject>();

	private int spawnedCount = 0;

	public void CreatePool (GameObject obj, int pSize, bool isScalable)
	{
		poolPrefab = obj;
		poolSize = pSize;
		scalable = isScalable;
		for (int i = 0; i < poolSize; i++) 
		{
			GameObject poolObj = Instantiate (poolPrefab);
			poolObj.transform.SetParent (this.transform);
			poolObj.SetActive (false);
			poolList.Add (poolObj);
		}
	}

	public GameObject Instantiate ()
	{
		if (spawnedCount >= poolSize && !scalable)
			return null;

		if (spawnedCount >= poolSize && scalable) 
		{
			GameObject newPoolObj = Instantiate (poolPrefab);
			newPoolObj.transform.SetParent (this.transform);
			newPoolObj.SetActive (true);
			poolList.Add (newPoolObj);
			return newPoolObj;
		}

		GameObject obj = poolList[0];
		for (int i = 0; i < poolSize; i++)
		{
			if (poolList [i].activeSelf == false)
			{
				obj = poolList [i];
				break;
			}
		}
		 
		obj.SetActive (true);
		spawnedCount++;
		return obj;
	}

	public void Destroy (GameObject obj)
	{
		if (!obj)
			return;
		obj.SetActive (false);
		spawnedCount--;
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
