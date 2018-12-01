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

public class SceneController : NetworkBehaviour {

    [SerializeField]
    public List<LevelNameAndInputManager> scenes = new List<LevelNameAndInputManager>();

    public string currentSceneName = "HubScene";

    // Use this for initialization
    void Start()
    {       

     
    }

    // Update is called once per frame
    void Update () {

        bool isDown = Input.GetKeyDown(KeyCode.Z);

        if( isDown )
        {
            Debug.Log("Z");
            GameObject.Find("NetworkController").GetComponent<NetworkManager>().ServerChangeScene("FrostRunner2");
        }
	}

    public void ChangeSceneName(string newSceneName)
    {
        currentSceneName = newSceneName;
    }

    public string GetCurrentSceneName()
    {
        return currentSceneName;
    }


}
