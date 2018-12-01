using System.Collections;
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
        var bullet = (GameObject)Instantiate(bulletPrefab, this.transform.position, this.transform.rotation);

        bullet.GetComponent<Rigidbody2D>().velocity.Set(Input.GetAxis("Horizontal") * 6, Input.GetAxis("Vertical") * 6);

        if (!isServer)
        {
           bullet.GetComponent<SpriteRenderer>().material.color = Color.green;
        }

        NetworkServer.Spawn(bullet);

        Destroy(bullet, 3.0f);
    }
}
