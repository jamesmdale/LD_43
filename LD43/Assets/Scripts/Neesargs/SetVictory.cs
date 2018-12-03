using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetVictory : MonoBehaviour
{
    private GameObject localPlayer;

    // Update is called once per frame
    void Update ()
    {
        FindPlayer();

        if( localPlayer != null )
        {
            // If I'm already lost
            int localChancesLeft = localPlayer.GetComponent<InputManager_JumpRope>().chancesLeft;
            if (localChancesLeft <= 0)
            {
                GetComponent<Text>().text = "You, LOSER!";
                return;
            }

            // If I'm not lost!
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

            // If all others are out of match
            bool otherAreOutOfMatch = true;

            if (players.Length == 1)
                otherAreOutOfMatch = false;

            foreach( GameObject player in players )
            {
                InputManager_JumpRope jrScript = player.GetComponent<InputManager_JumpRope>();

                if (jrScript.isLocalPlayer == false )
                {
                    if (jrScript.chancesLeft > 0)
                    {
                        otherAreOutOfMatch = false;
                        break;
                    }
                }
            }

            if(otherAreOutOfMatch)
            {
                GetComponent<Text>().text = "Victory!";
            }
        }
	}

    void FindPlayer()
    {
        if (localPlayer != null)
            return;

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject playerGO in players)
        {
            if (playerGO.GetComponent<InputManager_JumpRope>().isLocalPlayer)
                localPlayer = playerGO;
        }
    }
}
