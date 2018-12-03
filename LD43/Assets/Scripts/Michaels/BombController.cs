using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BombController : NetworkBehaviour
{
    [SyncVar]
    public float explosionTimer = 0.0f;
    public float maxTimeBeforeExplosion = 10.0f;

    public GameObject explosionPrefabReference;

	// Use this for initialization
	void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(!isServer)
        {
            return;
        }

        CmdSetExplosionTimer(Time.deltaTime);
        
        if (explosionTimer >= maxTimeBeforeExplosion)
        {
            CmdCreateExplosion();
           
        }
	}

    [Command]
    void CmdSetExplosionTimer(float spawnTime)
    {
        explosionTimer += spawnTime;
    }

    [Command]
    void CmdCreateExplosion()
    {
        Debug.LogFormat("Space Button pressed");

        var explosion = (GameObject)Instantiate(explosionPrefabReference, gameObject.transform.position, Quaternion.identity);
        NetworkServer.Spawn(explosion);
        NetworkServer.Destroy(gameObject);
    }
}
