using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Random = UnityEngine.Random;

public class InputManager_Frosty2 : NetworkBehaviour
{

	float speed = 6.0f;

	public KeyCode keyToPress;


	// Use this for initialization
	void Start()
	{
		keyToPress = GenerateRandomKeyCode();
	}

	// Update is called once per frame
	void Update()
	{
		if (!isLocalPlayer)
			return;

		if (Input.GetKeyDown(keyToPress))
		{
			DoMovement();
			ChangeKeyCode();
		}

		//Vector3 pos = this.gameObject.transform.position;
		//Camera.main.transform.position = new Vector3(pos.x, 0.0f, -10.0f);
	}

	void DoMovement()
	{
		this.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.right * speed);
		
		// maybe do some sweet sin up and down motion here
	}
	
	void ChangeKeyCode()
	{
		keyToPress = GenerateRandomKeyCode();
	}

	KeyCode GenerateRandomKeyCode()
	{

		int numberToUse = Random.Range(0, 26);
		
		if(numberToUse == 0 ) { return  KeyCode.A; }
		if(numberToUse == 1 ) { return  KeyCode.B; }
		if(numberToUse == 2 ) { return  KeyCode.C; }
		if(numberToUse == 3 ) { return  KeyCode.D; }
		if(numberToUse == 4 ) { return  KeyCode.E; }
		if(numberToUse == 5 ) { return  KeyCode.F; }
		if(numberToUse == 6 ) { return  KeyCode.G; }
		if(numberToUse == 7 ) { return  KeyCode.H; }
		if(numberToUse == 8 ) { return  KeyCode.I; }
		if(numberToUse == 9 ) { return  KeyCode.J; }
		if(numberToUse == 10 ) { return  KeyCode.K; }
		if(numberToUse == 11 ) { return  KeyCode.L; }
		if(numberToUse == 12 ) { return  KeyCode.M; }
		if(numberToUse == 13 ) { return  KeyCode.N; }
		if(numberToUse == 14 ) { return  KeyCode.O; }
		if(numberToUse == 15 ) { return  KeyCode.P; }
		if(numberToUse == 16 ) { return  KeyCode.Q; }
		if(numberToUse == 17 ) { return  KeyCode.R; }
		if(numberToUse == 18 ) { return  KeyCode.S; }
		if(numberToUse == 19 ) { return  KeyCode.T; }
		if(numberToUse == 20 ) { return  KeyCode.U; }
		if(numberToUse == 21 ) { return  KeyCode.V; }
		if(numberToUse == 22 ) { return  KeyCode.W; }
		if(numberToUse == 23 ) { return  KeyCode.X; }
		if(numberToUse == 24 ) { return  KeyCode.Y; }
		if(numberToUse == 25 ) { return  KeyCode.Z; }
		
		
		
		
		
		
		return KeyCode.A;
	}

}
