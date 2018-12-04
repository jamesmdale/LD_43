using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class GameController : MonoBehaviour
{

    public static GameController g_gameController = null;
    public PlayerWidgetController[] m_widgets;
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
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (PlayerWidgetController widget in m_widgets)
        {
            widget.gameObject.SetActive(false);
        }

        foreach (GameObject player in players)
        {
            PlayerController controller = player.GetComponent<PlayerController>();
            if (controller.m_playerID >= 0 && controller.m_playerID < m_widgets.Length)
            {
                m_widgets[controller.m_playerID].SetPlayer(controller);
                m_widgets[controller.m_playerID].gameObject.SetActive(true);
            } 
        }
    }

    public void AddPlayer(int playerID)
    {

        //Debug.Log("Setting player in canvas...");
        //if (playerID < m_widgets.Length && playerID >= 0)
        //{
        //    GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        //    foreach (GameObject player in players)
        //    {
        //        PlayerController controller = player.GetComponent<PlayerController>();
        //        if (controller.m_playerID == playerID)
        //        {
        //            m_widgets[playerID].SetPlayer(controller);
        //        }
        //    }
        //   // m_widgets[playerID].SetPlayerID(playerID);
        //} else
        //{
        //    Debug.Log("No widget for player: " + playerID);
        //}
    }
}
