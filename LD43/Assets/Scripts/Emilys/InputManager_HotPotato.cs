using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager_HotPotato : InputManager {
    GameObject player;

    // Use this for initialization
    void Start ()  
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public override void ProcessHorizontalAxis(float axis)
    {

    }

    public override void ProcessShift(bool isDown)
    {

    }

    public override void ProcessSpace(bool isDown)
    {
        if (isDown)
            Debug.Log("DOWN!");
    }

    public override void ProcessVerticalAxis(float axis)
    {

    }

}
