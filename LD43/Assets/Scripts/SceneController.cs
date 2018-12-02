using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public struct LevelNameAndInputManager
{
    public string sceneName;
    public string inputManagerScript;

    public LevelNameAndInputManager(string inSceneName, string inInputManagerScript)
    {
        sceneName = inSceneName;
        inputManagerScript = inInputManagerScript;
    }
}

public class SceneController : MonoBehaviour {

    [SerializeField]
    public List<LevelNameAndInputManager> scenes = new List<LevelNameAndInputManager>();

    public string currentSceneName = "HubScene";
    public static SceneController sceneControllerInstance;

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
    }

    public string GetCurrentSceneName()
    {
        return currentSceneName;
    }

    void GoToLevelCheck()
    {
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            currentSceneName = "FrostRunner2";
            NetworkManager.singleton.GetComponent<SceneController>().ChangeSceneName("FrostRunner2");
            NetworkManager.singleton.ServerChangeScene("FrostRunner2");
        }

        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            currentSceneName = "BomberMan";
            NetworkManager.singleton.GetComponent<SceneController>().ChangeSceneName("BomberMan");
            NetworkManager.singleton.ServerChangeScene("BomberMan");
        }

        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            currentSceneName = "JumpRope";
            NetworkManager.singleton.GetComponent<SceneController>().ChangeSceneName("JumpRope");

            NetworkManager.singleton.ServerChangeScene("JumpRope");
        }

        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            currentSceneName = "PotatoScene";
            NetworkManager.singleton.GetComponent<SceneController>().ChangeSceneName("PotatoScene");

            NetworkManager.singleton.ServerChangeScene("PotatoScene");
        }

        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            currentSceneName = "HubScene";
            NetworkManager.singleton.GetComponent<SceneController>().ChangeSceneName("HubScene");
            NetworkManager.singleton.ServerChangeScene("HubScene");
        }
    }
}
