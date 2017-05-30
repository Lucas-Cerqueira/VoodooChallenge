using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
	public GameObject poolPrefab;
	public int poolSize;
	public bool scalable;

	private List<GameObject> poolList = new List<GameObject>();

	private int spawnedCount = 0;

	private void CreatePool ()
	{
		print ("Criando pool " + poolPrefab.name);
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
		if (spawnedCount >= poolSize && !scalable || poolList.Count == 0)
			return null;

		if (spawnedCount >= poolSize && scalable) 
		{
			GameObject newPoolObj = Instantiate (poolPrefab);
			newPoolObj.transform.SetParent (this.transform);
			newPoolObj.SetActive (true);
			poolList.Add (newPoolObj);
			poolSize++;
			spawnedCount++;
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
	void Awake() 
	{
		CreatePool ();	
	}

	void Update()
	{
	}

}
