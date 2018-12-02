using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public struct SceneInputManagerName
{ 
    public string sceneName;
    public string inputManagerName;
}

public class ActivatorScript : MonoBehaviour {

    // Exposed Variables
    public List<SceneInputManagerName> scenes;
    public static ActivatorScript Instance { get; private set; }

    private int playersInNewScene = 0;
    private Dictionary<string, string> sceneAndInputScripts = new Dictionary<string, string>();

    private void Awake()
    {
        if( Instance == null )
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private void Update()
    {
        GameObject[] player = GameObject.FindGameObjectsWithTag("Player");
        
        if( playersInNewScene != player.Length )
        {
            // Debug.Log("FOUND PLAYERS! " + player.Length);
            playersInNewScene = player.Length;
            string sceneName = SceneManager.GetActiveScene().name;

            foreach ( GameObject go in player )
            {
                ChangeMyInputScripts(go, sceneName);
            }
        }
    }

    void Start ()
    {
        SceneManager.activeSceneChanged += ChangedActiveScene;

        foreach (SceneInputManagerName pair in scenes)
		{
            sceneAndInputScripts.Add(pair.sceneName, pair.inputManagerName);
		}
	}

    public void ChangeMyInputScripts( GameObject playerGO, string sceneName )
    {
        // Debug.Log("CALLED! ChangeMyInputScripts by " + playerGO.name + " for " + sceneName );

        // If we have that scene name in dictionary..
        bool hasScene = sceneAndInputScripts.ContainsKey(sceneName);
        if (hasScene == false)
        {
            Debug.LogError("Scene " + sceneName + ", not found!");
            return;
        }

        foreach (KeyValuePair<string, string> pair in sceneAndInputScripts)
        {
            // Scene name doesn't match
            if (pair.Key != sceneName)
            {
                // De-activate all other input manager scripts
                Behaviour behaviour = (Behaviour)playerGO.GetComponent(pair.Value);
                behaviour.enabled = false;
            }
            else
            {
                // Activate this scene's input manager
                Behaviour behaviour = (Behaviour)playerGO.GetComponent(pair.Value);
                behaviour.enabled = true;
            }
        }
    }

    private void ChangedActiveScene(Scene current, Scene next)
    {
        Debug.Log("CHNAGED active scene to " + next.name + ".." );

        playersInNewScene = 0;
    }
}
