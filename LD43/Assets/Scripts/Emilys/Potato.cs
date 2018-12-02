using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Potato : NetworkBehaviour {
    public float m_lifetime = 999.0f;
    float m_age = 0.0f;
    GameObject m_player;

	// Use this for initialization
	void Start () {
        m_lifetime = Random.Range(3.0f, 4.0f);
	}

    void SetLifetime(float lifetime)
    {
        m_lifetime = lifetime;
    }

    void SetPlayer(GameObject player)
    {
        m_player = player;
    }
	
	// Update is called once per frame
	void Update () {
        m_age += Time.deltaTime;
        if (m_age > m_lifetime)
        {
            Debug.Log("Kabooom");
            Destroy(gameObject);
        }
	}
}
