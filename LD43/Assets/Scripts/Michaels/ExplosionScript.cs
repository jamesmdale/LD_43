using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ExplosionScript : NetworkBehaviour {

    [SyncVar]
    float explosionTimer = 0.0f;
    float maxLifeTime = 0.25f;

    public float explosionRadius = 0.0f;

    [SyncVar]
    Vector3 explosionScale = new Vector3(1, 1, 1);
    float damageRadius = 0.75f;
    float maxScale = 5.0f;

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

        //get new scale amount
        float scaleAmount = RangeMapFloat(explosionTimer, 0.0f, maxLifeTime, 1.0f, maxScale);
        CmdSetScale(scaleAmount);

        // set scale
        gameObject.transform.localScale = explosionScale;

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        Debug.Log("Num players: " + players.Length);

        foreach(GameObject player in players)
        {
            float distance = Vector3.Distance(player.transform.position, gameObject.transform.position);

            Debug.Log("Distance to explosion center: " + distance);

            if (distance < damageRadius)
            {
                //player got hit by explosion
                Debug.Log("Player hit by explosion");
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

    [Command]
    void CmdSetScale(float scale)
    {
        explosionScale = new Vector3(scale, scale, 0.0f);
    }

    float RangeMapFloat(float inValue, float inStart, float inEnd, float outStart, float outEnd)
    {
        // If inRange is zero, call of this function is not-appropriate, handle this situation..
        if (inStart == inEnd)
        {
            return (outStart + outEnd) * 0.5f;
        }

        // Function call is appropriate, start calculation
        float inRange = inEnd - inStart;
        float outRange = outEnd - outStart;
        float inRelativeToStart = inValue - inStart;
        float fractionOfRange = inRelativeToStart / inRange;            // inRange can't be ZERO
        float outRelativeToStart = fractionOfRange * outRange;

        return outRelativeToStart + outStart;
    }
}
