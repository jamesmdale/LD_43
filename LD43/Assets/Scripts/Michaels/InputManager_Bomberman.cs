using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class InputManager_Bomberman : NetworkBehaviour
{
    [SyncVar]
    float delayTimeUntilNextBomb = 0.0f;

    float playerSpeed = 3.0f;
    public GameObject bombPrefabReference;

    [SyncVar]
    bool isPlayerElminated = false;

    // Use this for initialization
    void Start ()
    {
		
	}

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
            return;

        if (isPlayerElminated)
            return;

        float x = Input.GetAxis("Horizontal") * Time.deltaTime * playerSpeed;
        float y = Input.GetAxis("Vertical") * Time.deltaTime * playerSpeed;

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

        delayTimeUntilNextBomb = 1.0f;
    }

    [Command]
    public void CmdUpdateBombTimerDelay()
    {
        if (delayTimeUntilNextBomb < 0.0f)
        {
            delayTimeUntilNextBomb = 0.0f;
        }
        else
        {
            delayTimeUntilNextBomb -= Time.deltaTime;
        }
    }

    [Command]
    public void CmdSetPlayerEliminated()
    {
        isPlayerElminated = true;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }
}
