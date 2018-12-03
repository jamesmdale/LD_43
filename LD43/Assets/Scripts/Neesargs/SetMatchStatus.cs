using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetMatchStatus : MonoBehaviour
{

    private GameObject localPlayer;
    
	// Update is called once per frame
	void Update ()
    {

        FindPlayer();

        if (localPlayer != null)
        {
            if (localPlayer.GetComponent<InputManager_JumpRope>().chancesLeft > 0)
                GetComponent<Text>().text = "Match in-progress";
            else
                GetComponent<Text>().text = "You're out!";
        }
	}

    void FindPlayer()
    {
        if (localPlayer != null)
            return;

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        foreach( GameObject playerGO in players )
        {
            if (playerGO.GetComponent<InputManager_JumpRope>().isLocalPlayer)
                localPlayer = playerGO;
        }
    }

}
