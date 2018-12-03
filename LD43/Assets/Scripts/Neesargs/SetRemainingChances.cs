using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetRemainingChances : MonoBehaviour
{
    private GameObject localPlayer;
    
    // Update is called once per frame
    void Update ()
    {
        FindPlayer();

        if( localPlayer != null )
        {
            int chancesLeft = localPlayer.GetComponent<InputManager_JumpRope>().chancesLeft;
            GetComponent<Text>().text = "Chances left: " + chancesLeft;
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
 