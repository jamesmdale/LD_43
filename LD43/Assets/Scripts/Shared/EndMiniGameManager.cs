using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EndMiniGameManager : NetworkBehaviour {

	
	public float miniGameLength = 5.0f;
	public float timeBeforeSwapping = 5.0f;
	public GameObject timerPrefab;
	private bool spawnedUIElement = false;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		UpdateTimers();
		CheckIfEveryoneIsDone();
	}

	void UpdateTimers()
	{
		if (miniGameLength <= 0.0f)
		{
			CountdownBeforeGoingToHub();

			if (spawnedUIElement == false)
			{
				spawnedUIElement = true;
				SpawnUIFinished();
			}
		}
		else
		{
			miniGameLength -= Time.deltaTime;
		}
	}

	void CheckIfEveryoneIsDone()
	{
		GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

		bool areWeDone = true;
		foreach (GameObject player in players)
		{
			if (player.GetComponent<PlayerController>().isFinished == false)
			{
				areWeDone = false;
				Debug.Log("Someone is done");
			}
		}

		if (areWeDone)
		{
			EndMiniGame();
		}
	}

	void BackToHub()
	{
		NetworkManager.singleton.ServerChangeScene("HubScene");
		Destroy(this.gameObject);
	}

	void CountdownBeforeGoingToHub()
	{
		if (timeBeforeSwapping <= 0.0f)
		{
			BackToHub();
		}
		else
		{
			timeBeforeSwapping -= Time.deltaTime;
		}
	}
	
	void SpawnUIFinished()
	{
		GameObject UITimer = Instantiate(timerPrefab);
		
		NetworkServer.Spawn(UITimer);
	}

	
	// This might need to be a command
	public void EndMiniGame()
	{
		// we are already done
		if(miniGameLength <= 0.0f)
			return;
		
		if(spawnedUIElement == true)
			return;
	
		// end the game
		miniGameLength = 0.0f;
	}
}
