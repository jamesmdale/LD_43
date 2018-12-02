using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DisableNetworkGUI : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
		NetworkManager.singleton.GetComponent<NetworkManagerHUD>().showGUI = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
