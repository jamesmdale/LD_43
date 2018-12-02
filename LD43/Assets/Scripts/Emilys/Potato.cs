using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Potato : NetworkBehaviour {
    public float m_lifetime = 999.0f;
    float m_age = 0.0f;

    [SyncVar]
    int m_playerID;

    GameObject m_player;

	// Use this for initialization
	void Start () {
        m_lifetime = Random.Range(3.0f, 4.0f);
	}

    void SetLifetime(float lifetime)
    {
        m_lifetime = lifetime;
    }

    [Command]
    public void CmdSetPlayer(int playerID)
    {
        //if (!isServer)
        //{
        //    return;
        //}
        m_playerID = playerID;
        m_player = GetPlayerWithID(playerID) ;


        //transform.localPosition.Set(0.0f, .5f, 0.0f);
        Debug.Log("Following : " + playerID);

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

    // Update is called once per frame
    void Update () {
        m_age += Time.deltaTime;
        //if (m_age > m_lifetime)
        //{
        //    Debug.Log("Kabooom");
        //    Destroy(gameObject);
        //}

        //follow transform with player;

        if (m_player != null)
        {
            gameObject.transform.position = new Vector3(m_player.transform.position.x, m_player.transform.position.y + 0.5f, m_player.transform.position.z);
            gameObject.transform.parent = m_player.transform;
        }
    }
}
