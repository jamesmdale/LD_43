using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager_Frosty2 : InputManager {

    float speed = 6.0f;


	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!isLocalPlayer)
            return;

        Vector3 pos = this.gameObject.transform.position;
        Camera.main.transform.position = new Vector3(pos.x, 0.0f, -10.0f);
	}

    public override void ProcessSpace(bool isDown)
    {
        //throw new NotImplementedException();
    }

    public override void ProcessShift(bool isDown)
    {
        //throw new NotImplementedException();
    }

    public override void ProcessHorizontalAxis(float axis)
    {
        if (axis == 0.0f)
            return;


        float rate = 0.0f;

        if(axis < 0.0f)
        {
            rate = (Time.deltaTime * speed) * -1.0f;
        }
        else
        {
            rate = Time.deltaTime * speed;
        }

        

        transform.Translate(rate, 0, 0);

    }

    public override void ProcessVerticalAxis(float axis)
    {
        if (axis == 0.0f)
            return;


        float rate = 0.0f;

        if (axis < 0.0f)
        {
            rate = (Time.deltaTime * speed) * -1.0f;
        }
        else
        {
            rate = Time.deltaTime * speed;
        }



        transform.Translate(0, rate, 0);

    }
}
