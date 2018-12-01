using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //collision stuffs
    void OnCollisionEnter2D(Collision2D collision)
    {
        //Destroy(collision.gameObject);a
        Destroy(this.gameObject);
        Debug.Log("COLLIDED, BRUH");       
    }
}
