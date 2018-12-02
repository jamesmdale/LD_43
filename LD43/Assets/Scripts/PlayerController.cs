using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class PlayerController : NetworkBehaviour
{
    public string displayName = "CoolZac";

    // Use this for initialization
    void Start ()
    { 
        displayName = NetworkManager.singleton.GetComponent<StorePlayerName>().playerName;
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
}
