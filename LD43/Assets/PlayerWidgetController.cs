using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWidgetController : MonoBehaviour {
    public PlayerController m_player;
    string m_name;
    int m_playerID = -1;
    Text m_text;

	// Use this for initialization
	void Start () {
        m_text = gameObject.GetComponent<Text>();
        m_name = "PlayerName";
        SetText();
        SetVisibility();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetPlayerID(int playerID)
    {
        m_playerID = playerID;
        SetText();
        Debug.Log("Player text set to " + playerID);
    }

    public void SetPlayer(PlayerController player)
    {
        m_player = player;
        m_playerID = m_player.m_playerID;
        SetText();
    }

    void SetText()
    {
        if (m_player != null)
        {
            m_text.text = m_player.displayName + ": " + m_playerID;
        }
        SetVisibility();
    }

    void SetVisibility()
    {
        if (m_playerID < 0)
        {
            m_text.enabled = false;
        } else
        {
            m_text.enabled = true;
        }
    }
}
