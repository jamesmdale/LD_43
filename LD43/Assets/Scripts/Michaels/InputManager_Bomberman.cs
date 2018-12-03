using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class InputManager_Bomberman : NetworkBehaviour
{
    float m_playerSpeed = 3.0f;
    public GameObject bombPrefabReference;

    // Use this for initialization
    void Start ()
    {
		
	}

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
            return;

        float x = Input.GetAxis("Horizontal") * Time.deltaTime * m_playerSpeed;
        float y = Input.GetAxis("Vertical") * Time.deltaTime * m_playerSpeed;

        gameObject.transform.Translate(x, y, 0.0f);

        //check for input
        bool spaceJustPressed = Input.GetKeyDown(KeyCode.Space);
        if(spaceJustPressed)
        {
            CmdProcessSpace();
        }
    }

    [Command]
    public void CmdProcessSpace()
    {
        Debug.LogFormat("Space Button pressed");

        var bomb = (GameObject)Instantiate(bombPrefabReference, gameObject.transform.position, Quaternion.identity);
        NetworkServer.Spawn(bomb);
    }
}
