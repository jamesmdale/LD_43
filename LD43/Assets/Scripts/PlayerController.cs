using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


public class PlayerController : NetworkBehaviour
{
    [SyncVar]
    public string displayName = "INVALID_NAME";

    [SyncVar]
    public int m_playerID = -1;

    bool postStart = true;

    [SyncVar] 
    public bool isFinished = false;
    
    [SyncVar]
    public int spriteIndexToUse = -1;

    public GameObject m_arrow;

    void Start ()
    {
        if (!isLocalPlayer)
            return;
        
        postStart = true;
        //CmdSetPlayerID(NetworkManager.singleton.GetComponent<SacrificialNetworkManager>().m_localConnectionID);
        CmdUpdateDisplayName(displayName = NetworkManager.singleton.GetComponent<StorePlayerName>().playerName);
        CmdSetFinished(false);
    }

    private void OnDisable()
    {
        enabled = true;
    }

    public override void OnStartLocalPlayer()
    {
        //GetComponent<SpriteRenderer>().material.color = Color.blue;
        m_arrow.GetComponent<SpriteRenderer>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(displayName + " : " + isFinished);

        // if you wanna test
        if (Input.GetKeyDown(KeyCode.Keypad9))
        {
            SetPlayerAsFinished();
        }

        if (isFinished)
        {
            GetComponent<SpriteRenderer>().material.color = Color.black;
        }
        
        if (postStart)
        {
            if (connectionToClient != null)
            {
                CmdSetPlayerID((int)connectionToClient.connectionId);
                CmdSetSpriteIndex(spriteIndexToUse);
                postStart = false;
                
                //PlayerNameUIText.GetComponent<Text>().text = displayName;
            }
        }
        if (!isLocalPlayer)
            return;
        
        //PlayerNameUIText.GetComponent<Text>().text = displayName;
    }

    // This is how we can set the player as done in our mini games
    public void SetPlayerAsFinished()
    {
        if(!isLocalPlayer)
            return;

        CmdSetFinished(true);
    }
    
    // COMMANDS
    [Command]
    void CmdUpdateDisplayName(string playerName)
    {
        displayName = playerName;
    }
    
    [Command]
    void CmdSetFinished(bool finished)
    {
        isFinished = finished;
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

    [Command]
    public void CmdSetSpriteIndex(int spriteIndex)
    {
        spriteIndexToUse = spriteIndex;
    }
    
    
}
