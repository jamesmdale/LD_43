using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class PlayerController : NetworkBehaviour {

    public string currentSceneName = "HubScene";
   // public SceneController sceneController;
    private Dictionary<string, InputManager> sceneInputScripts = new Dictionary<string, InputManager>();
    public string displayName = "CoolZac";

    // Use this for initialization
    void Start ()
    {
        foreach (LevelNameAndInputManager thisSceneInfo in NetworkManager.singleton.GetComponent<SceneController>().scenes)
        {
            string levelName = thisSceneInfo.sceneName;
            string scriptName = thisSceneInfo.inputManagerScript;

            InputManager ipScript = (InputManager)GetComponent(scriptName);

            if (ipScript != null)
                sceneInputScripts.Add(levelName, ipScript);
            else
                Debug.LogErrorFormat("Can't find player named " + displayName + ". " + scriptName + "!");
        }

 
        displayName = NetworkManager.singleton.GetComponent<StorePlayerName>().playerName;
    }

    public override void OnStartLocalPlayer()
    {
       GetComponent<SpriteRenderer>().material.color = Color.blue;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        currentSceneName = NetworkManager.singleton.GetComponent<SceneController>().GetCurrentSceneName();

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        bool spaceDown = Input.GetKeyDown(KeyCode.Space);
        bool shiftDown = Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift);

        ProcessHorizontalAxis(x);
        ProcessVerticalAxis(y);

        ProcessSpace(spaceDown);
        ProcessShift(shiftDown);

        GoToLevelCheck();
    }

    public void ChangeCurrentScene(string newScene)
    {
        currentSceneName = newScene;
    }

    void ProcessHorizontalAxis(float value)
    {
        InputManager ipScript;
        sceneInputScripts.TryGetValue(currentSceneName, out ipScript);

        if (ipScript != null)
            ipScript.ProcessHorizontalAxis(value);
        else
            Debug.LogErrorFormat("Can't find InputManager for " + currentSceneName + "." );
    }

    void ProcessVerticalAxis(float value)
    {
        InputManager ipScript;
        sceneInputScripts.TryGetValue(currentSceneName, out ipScript);

        if (ipScript != null)
            ipScript.ProcessVerticalAxis(value);
        else
            Debug.LogErrorFormat("Can't find InputManager for " + currentSceneName + ".");
    }

    void ProcessSpace(bool isDown)
    {
        InputManager ipScript;
        sceneInputScripts.TryGetValue(currentSceneName, out ipScript);

        if (ipScript != null)
            ipScript.ProcessSpace(isDown);
        else
            Debug.LogErrorFormat("Can't find InputManager for " + currentSceneName + ".");
    }

    void ProcessShift(bool isDown)
    {
        InputManager ipScript;
        sceneInputScripts.TryGetValue(currentSceneName, out ipScript);

        if (ipScript != null)
            ipScript.ProcessShift(isDown);
        else
            Debug.LogErrorFormat("Can't find InputManager for " + currentSceneName + ".");
    }

    void GoToLevelCheck()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            currentSceneName = "FrostRunner2";
            NetworkManager.singleton.GetComponent<SceneController>().ChangeSceneName("FrostRunner2");
            NetworkManager.singleton.ServerChangeScene("FrostRunner2");
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            currentSceneName = "BomberMan";
            NetworkManager.singleton.GetComponent<SceneController>().ChangeSceneName("BomberMan");
            NetworkManager.singleton.ServerChangeScene("BomberMan");
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            currentSceneName = "JumpRope";
            NetworkManager.singleton.GetComponent<SceneController>().ChangeSceneName("JumpRope");

            NetworkManager.singleton.ServerChangeScene("JumpRope");
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            currentSceneName = "PotatoScene";
            NetworkManager.singleton.GetComponent<SceneController>().ChangeSceneName("PotatoScene");

            NetworkManager.singleton.ServerChangeScene("PotatoScene");
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            currentSceneName = "HubScene";
            NetworkManager.singleton.GetComponent<SceneController>().ChangeSceneName("HubScene");
            NetworkManager.singleton.ServerChangeScene("HubScene");
        }
    }
}
