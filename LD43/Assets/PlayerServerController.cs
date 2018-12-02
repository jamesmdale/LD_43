using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerServerController : NetworkBehaviour
{
    //hooks are fired when this value changes
    [SyncVar(hook = "OnSetDisplayName")]
    public string playerDisplayName = "unknown";

    //hooks are fired when this value changes
    [SyncVar(hook = "OnChangeLives")]
    public int livesRemaining = 3;

	// Use this for initialization
	void Start ()
    {
        if (!isServer)
            return;

        //playerDisplayName = NetworkManager.singleton.GetComponent<StorePlayerName>().playerName;
    }

    // Update is called once per frame
    void Update ()
    {
		
	}

    //set display name to be synced server side
    public void SetPlayerDisplayName(string name)
    {
        playerDisplayName = name;
    }

    //set player lives variable to be synced server side
    public void SetPlayerLives(int lives)
    {
        livesRemaining = lives;
    }

    public void DecrementPlayerLives()
    {
        livesRemaining -= 1;
    }

    //happens when variable is changed
    public void OnSetDisplayName(string name)
    {
        //TODO: Will update any ui showing name
    }

    //happens when variable is changed
    public void OnChangeLives(int lives)
    {
        //TODO: Will update any ui showing lives
    }
}
