using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MiniGameSwapperManager : NetworkBehaviour
{

	public string miniGameToGoTo;
	public float timer = 5.0f;
	public GameObject timerPrefab;
	private bool startCountdown = false;
	
	
	// Use this for initialization
	void Start ()
	{
		SelectMiniGame();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (startCountdown)
		{
			if (timer <= 0.0f)
			{
				SwapMiniGame();
			}
			else
			{
				timer -= Time.deltaTime;
			}
		}

		if (Input.GetKeyDown(KeyCode.Space))
		{
			if (isServer)
			{
				startCountdown = true;
				SpawnUITimers();
				Debug.Log("Starting countdown!");
			}
		}
		
		// this is so if we wanna go to a specific one we can
		//OverloadMiniGameSelection();
	}

	void SelectMiniGame()
	{
		string nextSceneName = "";
		int levelToGoTo = Random.Range(0, 4);
        
		if (levelToGoTo == 0)
			nextSceneName = "FrostRunner2";
		if (levelToGoTo == 1)
			nextSceneName = "BomberMan";
		if (levelToGoTo == 2)
			nextSceneName = "JumpRope";
		if (levelToGoTo == 3)
			nextSceneName = "PotatoScene";

		miniGameToGoTo = nextSceneName;
	}
	
	void SwapMiniGame()
	{
		NetworkManager.singleton.ServerChangeScene(miniGameToGoTo);
		Destroy(this.gameObject);
	}

	void SpawnUITimers()
	{
		GameObject UITimer = Instantiate(timerPrefab);

		//Transform theLevelNameText = UITimer.transform.GetChild(1);
		//theLevelNameText.GetComponent<Text>().text = "Next Mini Game: " + miniGameToGoTo;
		
		NetworkServer.Spawn(UITimer);
	}

	void OverloadMiniGameSelection()
	{
		string nextSceneName = "";
        
		if (Input.GetKeyDown(KeyCode.Keypad4))
			nextSceneName = "FrostRunner2";
		if (Input.GetKeyDown(KeyCode.Keypad1))
			nextSceneName = "BomberMan";
		if (Input.GetKeyDown(KeyCode.Keypad2))
			nextSceneName = "JumpRope";
		if (Input.GetKeyDown(KeyCode.Keypad3))
			nextSceneName = "PotatoScene";
		if (Input.GetKeyDown(KeyCode.Keypad0))
			nextSceneName = "HubScene";

		miniGameToGoTo = nextSceneName;
	}
}
