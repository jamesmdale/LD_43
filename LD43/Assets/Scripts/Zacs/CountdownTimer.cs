using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{

	public float timer = 5.0f;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (timer > 0.0f)
		{
			this.gameObject.GetComponent<Text>().text = "Starting in: " + timer;
			
			timer -= Time.deltaTime;
		}
		else
		{
			timer = 0.0f;
		}
	}
}
