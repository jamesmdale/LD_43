﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SetWhatKeyToPress : MonoBehaviour
{
	public GameObject playerInScene;
	
	// Use this for initialization
	void OnEnable ()
	{
		FindPlayer();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (playerInScene == null)
		{
			FindPlayer();
		}
		else
		{
			KeyCode whatToPress = playerInScene.GetComponent<InputManager_Frosty2>().keyToPress;
		
			this.GetComponent<Text>().text = "Press: " + whatToPress;
		}
		

	}

	void FindPlayer()
	{
		GameObject[] players;
		players = GameObject.FindGameObjectsWithTag("Player");
		
		Debug.Log("Length" + players.Length);

		foreach(GameObject currentPlayer in players)
		{
			bool islocal = currentPlayer.GetComponent<InputManager_Frosty2>().isLocalPlayer;

			if (islocal)
			{
				playerInScene = currentPlayer;
			}
		}
	}
}
