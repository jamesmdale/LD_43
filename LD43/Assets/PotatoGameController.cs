using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PotatoGameController : NetworkBehaviour {
    public Text m_tutorialText;
    public Text m_timerText;
    public float m_tutorialTime = 5.0f;
    public GameObject m_potatoPrefab;
    bool potatoSpawned = false;

    GameObject m_localPlayer = null;

    GameObject m_chosenPlayer;
    float m_age = 0.0f;
    float m_explodeTime = -1.0f;
    float m_respawnLength = 1.0f;

	// Use this for initialization
	void Start () {

        Debug.Log("Spawning Potato");
        SpawnPotato();

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach(GameObject player in players)
        {
            if (player.GetComponent<NetworkIdentity>().isLocalPlayer)
            {
                m_localPlayer = player;
                break;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (!potatoSpawned)
        {
            SpawnPotato();
        }
        m_age += Time.deltaTime;
        m_timerText.text = m_localPlayer.GetComponent<InputManager_HotPotato>().m_potatoTimer.ToString("#.00");
        if (m_age > m_tutorialTime && m_tutorialText.enabled)
        {
            //m_tutorialText.enabled = false;
            if (m_localPlayer != null)
            {
                m_tutorialText.enabled = false;

            }
            
        }
        if (m_localPlayer.GetComponent<InputManager_HotPotato>().m_potatoTimer <= 0.0f && m_explodeTime < 0.0f)
        {
            m_explodeTime = Time.time;
        }

        if (m_explodeTime > 0.0f)
        {
            if (Time.time - m_explodeTime > m_respawnLength ){
                m_explodeTime = -1.0f;
                
                //if (isServer)
                //{
                    GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
                    foreach(GameObject p in players)
                    {
                        p.GetComponent<InputManager_HotPotato>().Reset();
                    }
                //}
                SpawnPotato();
            }
        }

	}

   //[Command]
    void SpawnPotato()
    {
        if (NetworkServer.active)
        {
            //GameObject potato = GameObject.Instantiate(m_potatoPrefab);


            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            int randIndex = Random.Range(0, players.Length);
            GameObject markedplayer = players[randIndex];
            markedplayer.GetComponent<InputManager_HotPotato>().SetHasPotato(true);
           


            // Spawn the potato on the Clients
           // NetworkServer.Spawn(potato);
            // Destroy the bullet after 2 seconds
            //Destroy(potato, 2.0f);
            potatoSpawned = true;
        }
    }
}
