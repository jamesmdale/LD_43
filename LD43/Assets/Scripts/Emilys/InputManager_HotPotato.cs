using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager_HotPotato : InputManager {
    float m_minPotatoDistance = 3.0f;
    float m_playerSpeed = 5.0f;

    // Use this for initialization
    void Start ()  
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!isLocalPlayer)
            return;

        float x = Input.GetAxis("Horizontal") * Time.deltaTime * m_playerSpeed;
        float y = Input.GetAxis("Vertical") * Time.deltaTime * m_playerSpeed;

        gameObject.transform.Translate(x, y, 0.0f);

        if (Input.GetKeyDown("space"))
        {
            Debug.Log("Potato passin");
            PassPotato();
        }
    }

    public override void ProcessHorizontalAxis(float axis)
    {
        gameObject.transform.Translate(axis * Time.deltaTime * m_playerSpeed, 0.0f, 0.0f);
    }

    public override void ProcessShift(bool isDown)
    {

    }

    public override void ProcessSpace(bool isDown)
    {

    }

    public void PassPotato()
    { 
        GameObject[] m_players = GameObject.FindGameObjectsWithTag("Player");
        float minDistance = 9999.0f;
        GameObject minPlayer = null;
        foreach(GameObject player in m_players)
        {
            if (gameObject != player)
            {
                float dist = Vector3.Distance(player.transform.position, gameObject.transform.position);
                if (dist < minDistance)
                {
                    minDistance = dist;
                    minPlayer = player;
                }
            }
        }
        if (minPlayer != null && minDistance < m_minPotatoDistance)
        {
            TransferPotato(minPlayer);
        } else
        {
            Debug.Log("Not close enough for potato transfer");
        }
        
    }

    public override void ProcessVerticalAxis(float axis)
    {
        gameObject.transform.Translate(0.0f, axis * Time.deltaTime * m_playerSpeed, 0.0f);
    }

    public void TransferPotato(GameObject playerObject)
    {
        Potato potato = GetPotato();
        if (potato != null)
        {
            potato.CmdSetPlayer(playerObject);
        } else
        {
            Debug.Log("no potato today :(");
        }
    }


    Potato GetPotato()
    {
        return gameObject.GetComponentInChildren<Potato>();
    }
}
