using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public static GameController g_gameController = null;
    public Canvas m_canvas;
    public PlayerController[] m_players;

    //singleton AF
    void Awake()
    {
        //Check if instance already exists
        if (g_gameController == null)

            //if not, set instance to this
            g_gameController = this;

        //If instance already exists and it's not this:
        else if (g_gameController != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(this.gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(this.gameObject);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void AddPlayer(PlayerController controller)
    {

    }
}
