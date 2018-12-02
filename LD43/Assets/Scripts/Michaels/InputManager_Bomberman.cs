using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager_Bomberman : InputManager {

    float m_playerSpeed = 3.0f;

    // Use this for initialization
    void Start ()
    {
		
	}

    // Update is called once per frame
    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
            return;

        float x = Input.GetAxis("Horizontal") * Time.deltaTime * m_playerSpeed;
        float y = Input.GetAxis("Vertical") * Time.deltaTime * m_playerSpeed;

        gameObject.transform.Translate(x, y, 0.0f);
    }

    public override void ProcessHorizontalAxis(float axis)
    {
        transform.Translate(axis, 0, 0);
    }

    public override void ProcessVerticalAxis(float axis)
    {
        transform.Translate(0, axis, 0);
    }

    public override void ProcessShift(bool isDown)
    {
        string downUp = isDown ? "DOWN" : "UP";
        if (isDown)
            Debug.LogFormat("Shift Button = " + downUp);
    }

    public override void ProcessSpace(bool isDown)
    {
        string downUp = isDown ? "DOWN" : "UP";
        if (isDown)
        {
            Debug.LogFormat("Space Button = " + downUp);

            //SceneController sceneController = GameObject.Find("SceneManager").GetComponent<SceneController>();
            //sceneController.ChangeSceneName("HotPotato");
        }

    }
}
