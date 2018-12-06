using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

//[System.Serializable]
//public struct LevelNameAndInputManager
//{
//    public string sceneName;
//    public string inputManagerScript;

//    public LevelNameAndInputManager(string inSceneName, string inInputManagerScript)
//    {
//        sceneName = inSceneName;
//        inputManagerScript = inInputManagerScript;
//    }
//}

public class SacrificialNetworkManager : NetworkManager
{
    public int m_localConnectionID;
    //public List<LevelNameAndInputManager> scenes = new List<LevelNameAndInputManager>();
    //public string currentSceneName = "HubScene";
    //public static SceneController sceneControllerInstance;


    public int[] playerSpriteList = {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15};

    // Server callbacks
    public override void OnServerConnect(NetworkConnection conn)
    {
        base.OnServerConnect(conn);
        Debug.Log("A client connected to the server: " + conn);
    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        base.OnServerDisconnect(conn);
        //NetworkServer.DestroyPlayersForConnection(conn);

        //if (conn.lastError != NetworkError.Ok)
        //{

        //    if (LogFilter.logError) { Debug.LogError("ServerDisconnected due to error: " + conn.lastError); }

        //}

        Debug.Log("A client disconnected from the server: " + conn);

    }

    public override void OnServerReady(NetworkConnection conn)
    {
        base.OnServerReady(conn);
        //NetworkServer.SetClientReady(conn);

       // Debug.Log("Client is set to the ready state (ready to receive state updates): " + conn);

    }

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        //base.OnServerAddPlayer(conn, playerControllerId);
        GameObject player = (GameObject)GameObject.Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        //player.GetComponent<PlayerController>().CmdSetPlayerID(conn.connectionId);
        
        player.GetComponent<PlayerController>().spriteIndexToUse = GetSpriteFromHost(conn.connectionId);
        //player.GetComponent<PlayerController>().SetTheSpriteIndex(GetSpriteFromHost());
        
        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);

        Debug.Log("Client has requested to get his player added to the game pcontrollerid= " + playerControllerId + "connID= " + conn.connectionId);
        GameController.g_gameController.AddPlayer(conn.connectionId);

    }

    int GetSpriteFromHost(int connIndex)
    {
        return playerSpriteList[connIndex];
    }

    public override void OnServerRemovePlayer(NetworkConnection conn, UnityEngine.Networking.PlayerController player)
    {
        base.OnServerRemovePlayer(conn, player);
        // if (player.gameObject != null)
        //
        //     NetworkServer.Destroy(player.gameObject);
        //
        Debug.Log("Removing Player");
    }

    public override void OnServerError(NetworkConnection conn, int errorCode)
    {
        base.OnServerError(conn, errorCode);
        Debug.Log("Server network error occurred: " + (NetworkError)errorCode);

    }

    public override void OnStartHost()
    {
        base.OnStartHost();

        ShuffleList();
        //Debug.Log("Host has started");

    }

    void ShuffleList()
    {
        for (int i = 0; i < Random.Range(0, 100); i++)
        {
            int firstIndex = Random.Range(0, 16);
            int secondindex = Random.Range(0, 16);

            int temp = playerSpriteList[secondindex];
            playerSpriteList[secondindex] = playerSpriteList[firstIndex];
            playerSpriteList[firstIndex] = temp;
        }
    }

    public override void OnStartServer()
    {
        base.OnStartServer();
        //Debug.Log("Server has started");

    }

    public override void OnStopServer()
    {
        base.OnStopServer();
        //Debug.Log("Server has stopped");

    }

    public override void OnStopHost()
    {
        base.OnStopHost();
        //Debug.Log("Host has stopped");

    }

    // Client callbacks

    public override void OnClientConnect(NetworkConnection conn)

    {

        base.OnClientConnect(conn);

        m_localConnectionID = conn.connectionId;

        Debug.Log("Connected successfully to server, now to set up other stuff for the client... " + conn);

    }

    public override void OnClientDisconnect(NetworkConnection conn)
    {
        base.OnClientDisconnect(conn);
        Debug.Log("Client disconnected from server: " + conn);

    }

    public override void OnClientError(NetworkConnection conn, int errorCode)
    {
        base.OnClientError(conn, errorCode);
        Debug.Log("Client network error occurred: " + (NetworkError)errorCode);

    }

    public override void OnClientNotReady(NetworkConnection conn)
    {
        base.OnClientNotReady(conn);
        //Debug.Log("Server has set client to be not-ready (stop getting state updates)");

    }

    public override void OnStartClient(NetworkClient client)
    {
        base.OnStartClient(client);
        //Debug.Log("Client has started");

    }

    public override void OnStopClient()
    {
        base.OnStopClient();
       // Debug.Log("Client has stopped");

    }

    public override void OnClientSceneChanged(NetworkConnection conn)
    {
        base.OnClientSceneChanged(conn);

        Debug.Log("Server triggered scene change and we've done the same, do any extra work here for the client...");

    }

    //public void ChangeSceneName(string newSceneName)
    //{
    //    currentSceneName = newSceneName;
    //}

    //public string GetCurrentSceneName()
    //{
    //    return currentSceneName;
    //}

}