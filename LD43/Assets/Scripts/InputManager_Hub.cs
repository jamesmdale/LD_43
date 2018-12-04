using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class InputManager_Hub : NetworkBehaviour {
    float m_playerSpeed = 5.0f;
    GameObject gameStateManagerPrefabReference;

	// Use this for initialization
	void Start ()
    {
   
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!isLocalPlayer)
            return;

        float x = Input.GetAxis("Horizontal") * Time.deltaTime * m_playerSpeed;
        float y = Input.GetAxis("Vertical") * Time.deltaTime * m_playerSpeed;

        gameObject.transform.Translate(x, y, 0.0f);
        
        Vector3 pos = this.gameObject.transform.position;
        Camera.main.transform.position = new Vector3(pos.x, pos.y, -10.0f);
    }
}
