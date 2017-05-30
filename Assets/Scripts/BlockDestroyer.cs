using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BlockDestroyer : MonoBehaviour {

	public float delayBetweenDestroy = 0.5f;

	private BlockSpawner blockSpawnerScript;
	private Pool[] pools;
	private float elapsedTime;


	// Use this for initialization
	void Start () 
	{
		GameObject pool = GameObject.Find("Pool");
		blockSpawnerScript = pool.GetComponent<BlockSpawner> ();
		pools = pool.GetComponents<Pool> ();
		elapsedTime = delayBetweenDestroy;
	}

	// Update is called once per frame
	void Update () 
	{
		elapsedTime += Time.deltaTime;
	}

	void OnTriggerEnter2D(Collider2D other) 
	{
		if (other.gameObject.CompareTag("Block") || other.gameObject.CompareTag("ScoreTrigger")) 
		{
			if (other.gameObject.CompareTag("Block"))
				pools[0].Destroy (other.gameObject);
			else
				pools[1].Destroy (other.gameObject);
			
			if (elapsedTime >= delayBetweenDestroy && blockSpawnerScript) 
			{
				blockSpawnerScript.SpawnNextTopLine ();
				elapsedTime = 0;
			}
		}
	}
}
