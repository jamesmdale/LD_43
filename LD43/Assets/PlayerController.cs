﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class PlayerController : NetworkBehaviour {

    public GameObject bulletPrefab;

	// Use this for initialization
	void Start () {
		
	}

    public override void OnStartLocalPlayer()
    {
       GetComponent<SpriteRenderer>().material.color = Color.blue;
    }

    // Update is called once per frame
    void Update ()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 3.0f;
        var y = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

        transform.Translate(x, y, 0);


        if(Input.GetKeyDown(KeyCode.Space))
        {
            CmdFire();
        }
	}

    [Command]
    void CmdFire ()
    {
        Vector3 offset = new Vector3(.0f, 1.0f, 0.0f);
        var bullet = (GameObject)Instantiate(bulletPrefab, this.transform.position + offset, this.transform.rotation);

        Vector2 currentVelocity = GetComponent<Rigidbody2D>().velocity;
        currentVelocity.Normalize();

        bullet.GetComponent<Rigidbody2D>().velocity = currentVelocity * 6.0f;

        if (!isServer)
        {
           bullet.GetComponent<SpriteRenderer>().material.color = Color.green;
        }

        NetworkServer.Spawn(bullet);

        Destroy(bullet, 100.0f);
    }
}
