using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetPlayerNameUI : MonoBehaviour
{

	public GameObject PlayerObject;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		GetComponent<Text>().text = PlayerObject.GetComponent<PlayerController>().displayName;
	}
}
