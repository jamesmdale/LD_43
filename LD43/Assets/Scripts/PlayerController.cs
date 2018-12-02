using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class PlayerController : NetworkBehaviour
{
    public string displayName = "INVALID_NAME";

    void Start ()
    {
        if (!isLocalPlayer)
            return;

        //only server will run this
        CmdUpdateDisplayName(displayName = NetworkManager.singleton.GetComponent<StorePlayerName>().playerName);
    }

    public override void OnStartLocalPlayer()
    {
       GetComponent<SpriteRenderer>().material.color = Color.blue;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
            return;
    }

    [Command]
    void CmdUpdateDisplayName(string playerName)
    {
        displayName = playerName;
    }
}
