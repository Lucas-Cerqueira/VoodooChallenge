using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BlockDestroyer : MonoBehaviour {

	public float delayBetweenDestroy = 0.5f;

	private BlockSpawner blockSpawnerScript;
	private Pool poolScript;
	private float elapsedTime;


	// Use this for initialization
	void Start () 
	{
		GameObject pool = GameObject.Find("Pool");
		blockSpawnerScript = pool.GetComponent<BlockSpawner> ();
		poolScript = pool.GetComponent<Pool> ();
		elapsedTime = delayBetweenDestroy;
	}

	// Update is called once per frame
	void Update () 
	{
		elapsedTime += Time.deltaTime;
	}

	void OnTriggerEnter2D(Collider2D other) 
	{
		if (other.gameObject.tag == "Block") 
		{
			poolScript.Destroy (other.gameObject);
			if (elapsedTime >= delayBetweenDestroy) 
			{
				blockSpawnerScript.SpawnNextTopLine ();
				elapsedTime = 0;
			}
		}
	}
}
