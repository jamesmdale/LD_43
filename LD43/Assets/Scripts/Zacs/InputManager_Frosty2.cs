using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Random = UnityEngine.Random;

public class InputManager_Frosty2 : NetworkBehaviour
{

	float speed = 25.0f;
	float height = 2.5f;
	float offset = 0.0f;
	public bool hasDoneInputRecently = false;

	public float lengthOfFrictionTimer = 1.5f;
	public float frictionTimer;

	public KeyCode keyToPress;

	public bool isDead = false;
	//public GameObject wallToFollow;


	// Use this for initialization
	void Start()
	{
		keyToPress = GenerateRandomKeyCode();

		frictionTimer = lengthOfFrictionTimer;

		offset = Random.Range(0.0f, 1.0f);
		height = Random.Range(1.5f, 3.5f);
	}

	// Update is called once per frame
	void Update()
	{
		if (!isLocalPlayer)
			return;

		
		Vector3 pos = this.gameObject.transform.position;

		if (!isDead)
		{
			CheckForMissClick();

			if (Input.GetKeyDown(keyToPress))
			{
				DoMovement();
				ChangeKeyCode();
			}
		
			float y = Mathf.Cos(pos.x + offset) * height;
			this.gameObject.transform.position = new Vector3(pos.x, y, pos.z);


			ApplyFriction();
			MakeSureWeAreNotGoingBackwards();
		}

		Camera.main.transform.position = new Vector3(pos.x, 0.0f, -10.0f);
	}

	void ApplyFriction()
	{
		if (frictionTimer <= 0.0f)
		{
			if (hasDoneInputRecently == true)
			{
				hasDoneInputRecently = false;
			}
			else
			{
				this.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.left * (speed * .5f));
				frictionTimer = lengthOfFrictionTimer;
			}
		}
		else
		{
			frictionTimer -= Time.deltaTime;
		}
	}

	void MakeSureWeAreNotGoingBackwards()
	{
		if (this.gameObject.GetComponent<Rigidbody2D>().velocity.x <= 0.0f)
		{
			this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		}
	}

	void DoMovement()
	{
		this.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.right * speed);

		hasDoneInputRecently = true;

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
					this.gameObject.GetComponent<Rigidbody2D>().velocity * .6f;
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

	private void OnCollisionEnter2D(Collision2D other)
	{		
		if (other.gameObject.name == "Spikes")
		{
			PlayerDied();
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.name == "FinishLine")
		{
			this.gameObject.GetComponent<PlayerController>().SetPlayerAsFinished();	
		}
	}

	void PlayerDied()
	{
		isDead = true;
		this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		//this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
		
		this.gameObject.GetComponent<PlayerController>().SetPlayerAsFinished();
	}
}
