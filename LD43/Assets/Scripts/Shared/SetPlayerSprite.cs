using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlayerSprite : MonoBehaviour
{
	public Sprite[] playerSprites;
	
	// Use this for initialization
	void Start ()
	{
		int mySpriteIndex = this.gameObject.GetComponent<PlayerController>().spriteIndexToUse;
		this.gameObject.GetComponent<SpriteRenderer>().sprite = playerSprites[mySpriteIndex];
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
