using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class BombermanStateController : NetworkBehaviour
{
    // Use this for initialization
    [SyncVar]
    public GameObject textObjectReference;

	void Start ()
    {
        textObjectReference.GetComponent<Text>().enabled = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        Debug.Log("Num players: " + players.Length);

        foreach (GameObject player in players)
        {
            //float distance = Vector3.Distance(player.transform.position, gameObject.transform.position);

            //Debug.Log("Distance to explosion center: " + distance);

            //if (distance < damageRadius)
            //{
            //    //player got hit by explosion
            //    Debug.Log("Player hit by explosion");
            //    player.GetComponent<InputManager_Bomberman>().CmdSetPlayerEliminated();
            //}
        }

    }
}
