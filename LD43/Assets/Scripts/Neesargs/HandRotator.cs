using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HandRotator : NetworkBehaviour
{
    public GameObject rotateAround;
    public float rotationSpeed = 180.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!isServer)
            return;

        // Only host updates the position
        float deltaSec = Time.deltaTime;
        float rotation = rotationSpeed * deltaSec;
        transform.RotateAround( rotateAround.transform.position, new Vector3(0, 0, 1), rotation);
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isServer)
            return;

        GameObject cObject = collision.gameObject;
        if( cObject.tag == "Player" )
        {
            // Collided with a player
            InputManager_JumpRope script = cObject.GetComponent<InputManager_JumpRope>();

            if(script.ignoreCollision == false)
                script.RpcJustCollidedWithHand();
        }
    }
}
