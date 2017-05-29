using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
	private GameObject poolPrefab;
	private int poolSize;
	private List<GameObject> poolList = new List<GameObject>();

	private int spawnedCount = 0;

	public void CreatePool (GameObject obj, int pSize)
	{
		poolPrefab = obj;
		poolSize = pSize;
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
		if (spawnedCount >= poolSize)
			return null;
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
