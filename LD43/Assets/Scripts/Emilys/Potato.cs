using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Potato : NetworkBehaviour {
    public float m_lifetime = 999.0f;
    float m_age = 0.0f;
    [SyncVar]
    GameObject m_player;

	// Use this for initialization
	void Start () {
        m_lifetime = Random.Range(3.0f, 4.0f);
	}

    void SetLifetime(float lifetime)
    {
        m_lifetime = lifetime;
    }

    //[Command]
    public void CmdSetPlayer(GameObject player)
    {
        //if (!isServer)
        //{
        //    return;
        //}
        m_player = player;


        //transform.localPosition.Set(0.0f, .5f, 0.0f);
        Debug.Log("Following : " + player);

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
