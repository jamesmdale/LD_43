using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameStateController : NetworkBehaviour
{
    public static GameStateController Instance { get; private set; }

    public string hubLevel;

    [SerializeField]
    public List<string> miniGameScenes = new List<string>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (isServer)
            GoToLevelCheck();
	}
    
    void GoToLevelCheck()
    {
        string nextSceneName = "";
        
        if (Input.GetKeyDown(KeyCode.Keypad4))
            nextSceneName = "FrostRunner2";
        if (Input.GetKeyDown(KeyCode.Keypad1))
            nextSceneName = "BomberMan";
        if (Input.GetKeyDown(KeyCode.Keypad2))
            nextSceneName = "JumpRope";
        if (Input.GetKeyDown(KeyCode.Keypad3))
            nextSceneName = "PotatoScene";
        if (Input.GetKeyDown(KeyCode.Keypad0))
            nextSceneName = "HubScene";

        if (nextSceneName != "")
            NetworkManager.singleton.ServerChangeScene(nextSceneName);
    }

    [Command]
    public void CmdEndMiniGame(bool isServer)
    {
        Debug.Log("EndGame called by: [isServer = " + isServer.ToString() + "]");

        if (isServer)
            NetworkManager.singleton.ServerChangeScene(hubLevel);
    }
}
