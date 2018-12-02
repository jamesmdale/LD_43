using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ConnectMenuManager : MonoBehaviour {

    public GameObject   theNetworkManager;
    public Text         nameInputField;
    public Canvas       nameInputUI;
    public StorePlayerName     storePlayerScript;

    public string playerName;
    
    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void NameWasEntered()
    {
        playerName = nameInputField.text;

        if(playerName == "")
        {
            playerName = "Chris";
        }

        storePlayerScript.playerName = playerName;
        nameInputUI.enabled = false;
    }
}
