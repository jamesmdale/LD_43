using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Potato : NetworkBehaviour {
    public float m_lifetime = 999.0f;
    float m_age = 0.0f;
    bool m_spawned = false;
    int changeID = -1;

    [SyncVar(hook = "OnPotatoSwap")]
    public int m_playerID = -1;
    
    public GameObject m_player;

	// Use this for initialization
	void Start () {
        m_lifetime = Random.Range(3.0f, 4.0f);
        SpawnOnPlayer();
	}

    void OnPotatoSwap(int newID)
    {
        Debug.Log("PotatoSwapForLife " + newID);
        m_playerID = newID;
        changeID = newID;
        m_player = GetPlayerWithID(m_playerID);
        gameObject.transform.position = new Vector3(m_player.transform.position.x, m_player.transform.position.y + 0.5f, m_player.transform.position.z);
        gameObject.transform.parent = m_player.transform;
    }

    void SpawnOnPlayer()
    {

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        int randIndex = Random.Range(0, players.Length);
        GameObject markedplayer = players[randIndex];
        Debug.Log("sending potato to player " + markedplayer);
        SetPlayerFromID(markedplayer.GetComponent<PlayerController>().m_playerID);
        if (m_player != null)
        {
            m_spawned = true;
        }
    }

    void SetLifetime(float lifetime)
    {
        m_lifetime = lifetime;
    }

    //[Command]
    public void CmdSetPlayer(int playerID)
    {
        if (!isServer)
        {
            Debug.Log("Tried to assign potato to" + playerID + " (was " + m_playerID + ") on a client");
            return;
        }
        m_playerID = playerID;

        RpcSetPotatoParent();
        //m_player = GetPlayerWithID(playerID) ;
        //m_player.GetComponent<NetworkIdentity>().AssignClientAuthority(this.GetComponent<NetworkIdentity>().connectionToClient);

        //transform.localPosition.Set(0.0f, .5f, 0.0f);
        Debug.Log("Following : " + playerID);

    }

    [ClientRpc]
    void RpcSetPotatoParent()
    {
        //if (isLocalPlayer)
        //{
            m_player = GetPlayerWithID(m_playerID);
            gameObject.transform.position = new Vector3(m_player.transform.position.x, m_player.transform.position.y + 0.5f, m_player.transform.position.z);
            gameObject.transform.parent = m_player.transform;
            Debug.Log("RPC potato set");
        //}
    }

    GameObject GetPlayerWithID(int playerID)
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach(GameObject p in players)
        {
            if (p.GetComponent<PlayerController>().m_playerID == playerID)
            {
                return p;
            }
        }
        return null;
    }

    public void SetPlayerFromID(int playerID)
    {
        Debug.Log("Setting player id from WITHIN THE POTATO (ID = " + playerID + ")");
        changeID = playerID;
    }

    // Update is called once per frame
    void Update () {
        if (!m_spawned)
        {
            SpawnOnPlayer();
        }

        if (changeID >= 0)
        {
            Debug.Log("Changing Players to " + changeID);
            m_playerID = changeID;
            changeID = -1;
        }



        m_age += Time.deltaTime;
        
        //if (m_age > m_lifetime)
        //{
        //    Debug.Log("Kabooom");
        //    Destroy(gameObject);
        //}

        //follow transform with player;

        if (m_player != null)
        {
            if (m_player.GetComponent<PlayerController>().m_playerID != m_playerID)
            {
                m_player = GetPlayerWithID(m_playerID);
            }
            if (m_player != null)
            {
                //m_player.GetComponent<NetworkIdentity>().AssignClientAuthority(this.GetComponent<NetworkIdentity>().connectionToClient);
                gameObject.transform.position = new Vector3(m_player.transform.position.x, m_player.transform.position.y + 0.5f, m_player.transform.position.z);
                gameObject.transform.parent = m_player.transform;
            }
        } else
        {
            m_player = GetPlayerWithID(m_playerID);
            if (m_player != null)
            {
                //m_player.GetComponent<NetworkIdentity>().AssignClientAuthority(this.GetComponent<NetworkIdentity>().connectionToClient);
                gameObject.transform.position = new Vector3(m_player.transform.position.x, m_player.transform.position.y + 0.5f, m_player.transform.position.z);
                gameObject.transform.parent = m_player.transform;
            }
        }
    }
}
