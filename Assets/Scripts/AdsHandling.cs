using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsHandling : MonoBehaviour 
{

	public void ShowRewardedAd()
	{
		if (Advertisement.IsReady ("video")) 
		{
			Debug.Log ("Showing ad");
			ShowOptions options = new ShowOptions { resultCallback = HandleShowResult };
			Advertisement.Show ("video", options);
		} 
		else
			Debug.Log ("Ad not ready");
	}

	private void HandleShowResult (ShowResult result)
	{
		switch (result) 
		{
		case ShowResult.Finished:
			Debug.Log ("Ad succesfully shown");
			break;
		case ShowResult.Skipped:
			Debug.Log ("Ad was skipped");
			break;
		case ShowResult.Failed:
			Debug.Log ("Ad failed");
			break;
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
	}
}
