﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager_HotPotato : InputManager {
    GameObject player;
    float m_minPotatoDistance = 3.0f;

    // Use this for initialization
    void Start ()  
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (GetComponentInParent<PlayerController>().currentSceneName != "PotatoScene")
        {
            return;
        }
    }

    public override void ProcessHorizontalAxis(float axis)
    {

    }

    public override void ProcessShift(bool isDown)
    {

    }

    public override void ProcessSpace(bool isDown)
    {
        if (isDown)
        {
            GameObject[] m_players = GameObject.FindGameObjectsWithTag("Player");
            float minDistance = 9999.0f;
            GameObject minPlayer = null;
            foreach(GameObject player in m_players)
            {
                float dist = Vector3.Distance(player.transform.position, gameObject.transform.position);
                if (dist < minDistance)
                {
                    minDistance = dist;
                    minPlayer = player;
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
    }

    public override void ProcessVerticalAxis(float axis)
    {

    }

    public void TransferPotato(GameObject playerObject)
    {

    }

}
