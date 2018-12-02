using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotatoGameController : MonoBehaviour {
    public Text m_tutorialText;
    public float m_tutorialTime = 5.0f;


    GameObject m_chosenPlayer;
    float m_age = 0.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        m_age += Time.deltaTime;
        if (m_age > m_tutorialTime && m_tutorialText.enabled)
        {
            m_tutorialText.enabled = false;
        }

	}
}
