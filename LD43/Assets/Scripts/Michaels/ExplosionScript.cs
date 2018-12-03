using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ExplosionScript : NetworkBehaviour {

    [SyncVar]
    public float explosionTimer = 0.0f;
    public float maxLifeTime = 1.0f;

    public float explosionRadius = 0.0f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //only the server updates explosions
        if (!isServer)
        {
            return;
        }

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        foreach(GameObject player in players)
        {
            float distance = Vector3.Distance(player.transform.position, gameObject.transform.position);
            
            if(distance < explosionRadius)
            {
                //player got hit by explosion
            }
        }

        CmdSetExplosionTimer(Time.deltaTime);

        //if we are past our max life time, delete the explosion.
        if (explosionTimer >= maxLifeTime)
        {
            CmdDeleteExplosion();
        }
    }

    [Command]
    void CmdSetExplosionTimer(float deltaTime)
    {
        explosionTimer += deltaTime;
    }

    [Command]
    void CmdDeleteExplosion()
    {
        NetworkServer.Destroy(gameObject);
    }
}
