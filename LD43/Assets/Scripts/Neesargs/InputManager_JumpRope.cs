using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager_JumpRope : InputManager {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (GetComponentInParent<PlayerController>().currentSceneName != "JumpRope")
        {
            return;
        }
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
