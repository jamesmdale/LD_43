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

    void SetText()
    {
        m_text.text = m_name + ": " + m_playerID;
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
