using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class PlayerController : NetworkBehaviour
{
    public string displayName = "INVALID_NAME";

    [SyncVar]
    public int m_playerID = -1;

    void Start ()
    {
        if (!isLocalPlayer)
            return;

        CmdSetPlayerID((int) netId.Value);
        //CmdSetPlayerID(NetworkManager.singleton.GetComponent<SacrificialNetworkManager>().m_localConnectionID);
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

    [Command]
    public void CmdSetPlayerID(int playerID)
    {
        if (!isServer)
        {
            Debug.Log("I am not the server.");
            return;
        } else
        {
            m_playerID = playerID;
            Debug.Log("Assigning player id " + m_playerID + " on server");
        }
    }
}
