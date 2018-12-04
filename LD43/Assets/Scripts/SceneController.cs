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

	}

    public void ChangeScene(string newSceneName)
    {
        currentSceneName = newSceneName;
        NetworkManager.singleton.ServerChangeScene(currentSceneName);
    }

    public string GetCurrentSceneName()
    {
        return currentSceneName;
    }
}
