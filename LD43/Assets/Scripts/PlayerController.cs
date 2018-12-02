using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class PlayerController : NetworkBehaviour
{
    [SyncVar]
    public string displayName = "CoolZac";

    [SyncVar]
    public int m_playerID = -1;

    //public GameObject playerServerData;

   // [SyncVar]
    //public GameObject myPlayerServerDataReference;

    // Use this for initialization
    void Start ()
    {
        if (!isLocalPlayer)
            return;

        displayName = NetworkManager.singleton.GetComponent<StorePlayerName>().playerName;
        CmdSetPlayerID(NetworkManager.singleton.GetComponent<SacrificialNetworkManager>().m_localConnectionID);

        //CmdGeneratePlayerServerData();
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
    void CmdGeneratePlayerServerData()
    {
        //NetworkServer.Spawn(playerServerData);

        //myPlayerServerDataReference = (GameObject)Instantiate(playerServerData)
        //myPlayerServerDataReference.transform.parent = gameObject.transform;

        //myPlayerServerDataReference.GetComponent<PlayerServerController>().SetPlayerDisplayName(displayName);
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
