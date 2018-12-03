using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.Networking;

public class MovingWallScript : NetworkBehaviour
{
	public float speed;
	public float startDelay = 1.0f;
	public float currentTime;
	public float finishTime = 10.0f;

	public AnimationCurve speedUpCurve;
	
	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{
		if (currentTime > startDelay)
		{
			Vector3 oldPos = this.transform.position;
		
			float newX = (speed * Time.deltaTime);
			//this.transform.position = new Vector3(newX, oldPos.y, oldPos.z);
		
			this.transform.Translate(newX,0.0f, 0.0f);
		}

		currentTime += Time.deltaTime;


		speed = speedUpCurve.Evaluate((currentTime));
	}
}
