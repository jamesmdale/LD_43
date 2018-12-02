using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputManager_JumpRope : InputManager
{
    private float m_playerSpeed = 5.0f;

    // When this script gets enabled
    private void OnEnable()
    {

    }

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
    }

    public override void ProcessSpace(bool isDown)
    {
       
    }

    public override void ProcessShift(bool isDown)
    {

    }

    public override void ProcessHorizontalAxis(float axis)
    {

    }

    public override void ProcessVerticalAxis(float axis)
    {

    }
}
