using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PotatoGameController : NetworkBehaviour {
    public Text m_tutorialText;
    public float m_tutorialTime = 5.0f;
    public GameObject m_potatoPrefab;


    GameObject m_chosenPlayer;
    float m_age = 0.0f;

	// Use this for initialization
	void Start () {
       
        CmdSpawnPotato();
	}
	
	// Update is called once per frame
	void Update () {
        m_age += Time.deltaTime;
        if (m_age > m_tutorialTime && m_tutorialText.enabled)
        {
            m_tutorialText.enabled = false;
        }

	}

    [Command]
    void CmdSpawnPotato()
    {
        GameObject potato = GameObject.Instantiate(m_potatoPrefab);




        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        int randIndex = Random.Range(0, players.Length);
        GameObject markedplayer = players[randIndex];
        potato.GetComponent<Potato>().CmdSetPlayer(markedplayer);

        // Spawn the potato on the Clients
        NetworkServer.Spawn(potato);
        // Destroy the bullet after 2 seconds
        //Destroy(potato, 2.0f);
    }
}
