using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EndMiniGameManager : NetworkBehaviour {

	
	public float miniGameLength = 5.0f;
	public float timeBeforeSwapping = 5.0f;
	public GameObject timerPrefab;
	private bool spawnedUIElement = false;

	public bool checkForOnePersonVictory = true;
	public bool running = false;
	public float startDelayTimer = 2.0f;
	public float timeBeforeUIPopsUpAtEnd = 1.5f;
	public bool endingStarted = false;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		// the start delay is just to wait till all players join
		if (startDelayTimer <= 0.0f)
		{
			UpdateTimers();

			if (checkForOnePersonVictory)
				CheckIfSomeONEWon();
			else
				CheckIfEveryoneIsDone();
		}
		else
		{
			startDelayTimer -= Time.deltaTime;
		}
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

	void CheckIfSomeONEWon()
	{
		GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

		int playersLeftAlive = 0;
		foreach (GameObject player in players)
		{
			if (player.GetComponent<PlayerController>().isFinished == false)
				playersLeftAlive++;
		}
		
		// a person won!
		if (playersLeftAlive == 1)
		{
			if (endingStarted == false)
			{
				StartCoroutine(StartEndOfMiniGame());
				endingStarted = true;
			}
		}
			
	}

	IEnumerator StartEndOfMiniGame()
	{
		yield return new WaitForSeconds(timeBeforeUIPopsUpAtEnd);
		EndMiniGame();
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

	
	// This is local for the host
	private void EndMiniGame()
	{
		// we are already done
		if(miniGameLength <= 0.0f)
			return;
		
		if(spawnedUIElement == true)
			return;
	
		// end the game
		miniGameLength = 0.0f;
	}

	[Command]
	public void CmdTellHostToEndMiniGame()
	{
		if (!isServer)
			return;
		
		EndMiniGame();
	}
}
