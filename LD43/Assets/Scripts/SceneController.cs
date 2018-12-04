using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SceneController : MonoBehaviour
{
    public string currentSceneName = "HubScene";
    public static SceneController sceneControllerInstance { get; private set; }

    void Awake()
    {
        if (sceneControllerInstance == null)
        {
            sceneControllerInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start()
    {       
     
    }

    // Update is called once per frame
    void Update ()
    {
        GoToLevelCheck();
	}

    public void ChangeSceneName(string newSceneName)
    {
        currentSceneName = newSceneName;
        NetworkManager.singleton.ServerChangeScene(currentSceneName);
    }

    public string GetCurrentSceneName()
    {
        return currentSceneName;
    }

    void GoToLevelCheck()
    { 
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            ChangeSceneName("FrostRunner2");
        }

        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            ChangeSceneName("BomberMan");
        }

        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            ChangeSceneName("JumpRope");
        }

        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            ChangeSceneName("PotatoScene");
        }

        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            ChangeSceneName("HubScene");
        }
    }
}
