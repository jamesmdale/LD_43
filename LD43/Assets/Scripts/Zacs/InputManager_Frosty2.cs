using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Random = UnityEngine.Random;

public class InputManager_Frosty2 : NetworkBehaviour
{

	float speed = 25.0f;

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
		
		CheckForMissClick();

		if (Input.GetKeyDown(keyToPress))
		{
			DoMovement();
			ChangeKeyCode();
		}

		Vector3 pos = this.gameObject.transform.position;
		Camera.main.transform.position = new Vector3(pos.x, 0.0f, -10.0f);
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

	void CheckForMissClick()
	{
		if (Input.anyKeyDown)
		{
			if (!Input.GetKeyDown(keyToPress))
			{
				Debug.Log("Wrong Key Pressed!");
				//this.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.left * speed * .5f);
				//this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 0.0f);
				this.gameObject.GetComponent<Rigidbody2D>().velocity = 
					this.gameObject.GetComponent<Rigidbody2D>().velocity * .3f;
			}
		}
	}

	KeyCode GenerateRandomKeyCode()
	{

		int numberToUse = Random.Range(0, 26);

		KeyCode value = KeyCode.A;
		
		if(numberToUse == 0 ) { value =  KeyCode.A; }
		if(numberToUse == 1 ) { value =  KeyCode.B; }
		if(numberToUse == 2 ) { value =  KeyCode.C; }
		if(numberToUse == 3 ) { value =  KeyCode.D; }
		if(numberToUse == 4 ) { value =  KeyCode.E; }
		if(numberToUse == 5 ) { value =  KeyCode.F; }
		if(numberToUse == 6 ) { value =  KeyCode.G; }
		if(numberToUse == 7 ) { value =  KeyCode.H; }
		if(numberToUse == 8 ) { value =  KeyCode.I; }
		if(numberToUse == 9 ) { value =  KeyCode.J; }
		if(numberToUse == 10 ) { value =  KeyCode.K; }
		if(numberToUse == 11 ) { value =  KeyCode.L; }
		if(numberToUse == 12 ) { value =  KeyCode.M; }
		if(numberToUse == 13 ) { value =  KeyCode.N; }
		if(numberToUse == 14 ) { value =  KeyCode.O; }
		if(numberToUse == 15 ) { value =  KeyCode.P; }
		if(numberToUse == 16 ) { value =  KeyCode.Q; }
		if(numberToUse == 17 ) { value =  KeyCode.R; }
		if(numberToUse == 18 ) { value =  KeyCode.S; }
		if(numberToUse == 19 ) { value =  KeyCode.T; }
		if(numberToUse == 20 ) { value =  KeyCode.U; }
		if(numberToUse == 21 ) { value =  KeyCode.V; }
		if(numberToUse == 22 ) { value =  KeyCode.W; }
		if(numberToUse == 23 ) { value =  KeyCode.X; }
		if(numberToUse == 24 ) { value =  KeyCode.Y; }
		if(numberToUse == 25 ) { value =  KeyCode.Z; }


		if (value == keyToPress)
			value = GenerateRandomKeyCode();
			
		return value;
	}

}
