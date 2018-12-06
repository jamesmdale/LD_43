using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class InputManager_HotPotato : InputManager {
    float m_minPotatoDistance = 2.0f;
    float m_playerSpeed = 5.0f;
    public GameObject m_potatoPrefab;
    public GameObject m_explosionPrefab;
    bool m_canMove = true;

    public GameObject m_potatoBaby = null;

    [SyncVar(hook = "OnPotatoChange")]
    public bool m_playerHasPotato = false;
    [SyncVar(hook = "OnTimerChange")]
    public float m_potatoTimer = 8.0f;

    float m_potatoStartTime = 8.0f;

    //[SyncVar(hook ="OnExplodey")]
    bool m_gotExploded = false;
    Color m_defaultColor;

    // Use this for initialization
    void Start()
    {
        m_potatoTimer = m_potatoStartTime;
        m_defaultColor = gameObject.GetComponent<SpriteRenderer>().material.color;
    }

    public void Reset()
    {
        m_potatoTimer = m_potatoStartTime;
        gameObject.GetComponent<SpriteRenderer>().material.color = m_defaultColor;
    }


    // Update is called once per frame
    void Update()
    {
        
        //potato visualization
        if (m_playerHasPotato)
        {
            //Debug.Log("Checkin taht potato..." + m_potatoBaby);
            if (m_potatoBaby == null)
            {
                m_potatoBaby = GameObject.Instantiate(m_potatoPrefab, gameObject.transform);
                Debug.Log("WE GOTTA MAKE THIS BABY POTATO");
            }
        } else
        {
            if (m_potatoBaby != null)
            {
                //you just had the potato, and now you shouldn't.
                Destroy(m_potatoBaby);
                Debug.Log("WE GOTTA DESTROY THIS BABY POTATO");
            }
        }

        if (m_gotExploded)
        {
            OnExplodey(true);
            m_gotExploded = false;
        }

        UpdatePotatoTimer(Time.deltaTime);

        if (!isLocalPlayer)
            return;

        if (m_canMove)
        {
            //input logic
            float x = Input.GetAxis("Horizontal") * Time.deltaTime * m_playerSpeed;
            float y = Input.GetAxis("Vertical") * Time.deltaTime * m_playerSpeed;

            gameObject.transform.Translate(x, y, 0.0f);
        }




        

        if (Input.GetKeyDown("space"))
        {
            Debug.Log("Potato passin");
            CmdPassPotato();
        }

        ////potato logic
        //if (m_playerHasPotato)      //have potato on server
        //{
        //    UpdatePotatoTimer(Time.deltaTime);
        //    if (m_potatoTimer <= 0.0f)
        //    {
        //        Debug.Log("Kaboom");
        //        m_potatoTimer = 99.0f;
        //    }


        //} else //don't have potato on server
        //{

        //}
    }

    //public void SetHasPotato(bool hasPotato)
    //{
    //    if (!isServer)
    //        return;
    //    m_playerHasPotato = hasPotato;
    //}

    //[Command]
    public void SetHasPotato(bool hasPotato)
    {
        if (!isServer)
            return;

        m_playerHasPotato = hasPotato;
        RpcSetHasPotato(hasPotato);
        //if (currentHealth <= 0)
        //{
        //    currentHealth = maxHealth;
        //
        //    // called on the Server, but invoked on the Clients
        //    RpcRespawn();
        //}
    }


    [ClientRpc]
    void RpcSetHasPotato(bool hasPotato)
    {
        m_playerHasPotato = hasPotato;
    }

    [ClientRpc]
    void RpcSetPotatoTimer(float potatoTimer)
    {
        m_potatoTimer = potatoTimer;
        //Debug.Log("RPC Setting timer");
    }

    void UpdatePotatoTimer(float ds)
    {
        if (m_potatoTimer > 0.0f)
        {
            m_potatoTimer -= ds;
        } else if (m_potatoTimer <= 0.0f)
        {
            if (!m_gotExploded)
            {
                //Debug.Log("Kaboom");
                //m_potatoTimer = m_potatoStartTime;
                //if (isServer)
                //{
                if (m_playerHasPotato)
                {
                    m_gotExploded = true;
                    
                }
            }
            //}
        }


        if (!isServer)
            return;



        // Debug.Log("Updating timer");

        RpcSetPotatoTimer(m_potatoTimer);

    }

    void OnPotatoChange(bool hasPotato)
    {
        m_playerHasPotato = hasPotato;
    }

    //not too sure
    void OnTimerChange(float timer)
    {
        m_potatoTimer = timer;
    }

    void OnExplodey(bool val)
    {
        Debug.Log("I just exploded; " + val);
        m_gotExploded = val;
        GameObject.Instantiate(m_explosionPrefab, m_potatoBaby.transform.position, m_potatoBaby.transform.rotation) ;
        Destroy(m_potatoBaby);
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        m_canMove = false;
        m_playerHasPotato = false;
        //m_gotExploded = false;
        
        
        // this is how you mark them as done and when everyone is done the game ends
        //this.gameObject.GetComponent<PlayerController>().TellHostToEndMiniGame();
        StartCoroutine(EndTheGame());
    }

    IEnumerator EndTheGame()
    {
        yield return new WaitForSeconds(1.5f);
        this.gameObject.GetComponent<PlayerController>().TellHostToEndMiniGame();
    }


    public override void ProcessHorizontalAxis(float axis)
    {
        gameObject.transform.Translate(axis * Time.deltaTime * m_playerSpeed, 0.0f, 0.0f);
    }

    public override void ProcessShift(bool isDown)
    {

    }

    public override void ProcessSpace(bool isDown)
    {

    }

    [Command]
    public void CmdPassPotato()
    {
        if (!isServer)
            return;

        GameObject[] m_players = GameObject.FindGameObjectsWithTag("Player");
        float minDistance = 9999.0f;
        GameObject minPlayer = null;
        foreach (GameObject player in m_players)
        {
            if (gameObject != player)
            {
                float dist = Vector3.Distance(player.transform.position, gameObject.transform.position);
                if (dist < minDistance)
                {
                    minDistance = dist;
                    minPlayer = player;
                }
            }
        }
        if (minPlayer != null && minDistance < m_minPotatoDistance)
        {
            TransferPotato(minPlayer);
        } else
        {
            Debug.Log("Not close enough for potato transfer");
        }

    }

    public override void ProcessVerticalAxis(float axis)
    {
        gameObject.transform.Translate(0.0f, axis * Time.deltaTime * m_playerSpeed, 0.0f);
    }

    public void TransferPotato(GameObject playerObject)
    {
        //Potato potato = GetPotato();
        if (m_playerHasPotato)
        {
            int id = playerObject.GetComponent<PlayerController>().m_playerID;
            Debug.Log("Passing to player: " + id);
            CmdPassToPlayer(id);
            //CmdSetHasPotato(false);
            //playerObject.GetComponentInChildren<InputManager_HotPotato>().CmdSetHasPotato(true);
            //potato.SetPlayerFromID(id);
        } else
        {
            Debug.Log("no potato today :(");
        }
    }

    [Command]
    void CmdPassToPlayer(int id)
    {
        if (!isServer)
            return;

        bool alreadyPassed = false;
        GameObject[] m_players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in m_players)
        {
            if (player.GetComponent<PlayerController>().m_playerID == id)
            {
                if (!alreadyPassed)
                {
                    //player.GetComponent<InputManager_HotPotato>().m_playerHasPotato = true;
                    //player.GetComponent<InputManager_HotPotato>().m_potatoTimer = m_potatoTimer;
                    player.GetComponent<InputManager_HotPotato>().SetHasPotato(true);
                    player.GetComponent<InputManager_HotPotato>().RpcSetPotatoTimer(m_potatoTimer);
                    //m_playerHasPotato = false;
                    //RpcSetHasPotato(true);
                    //break;
                    alreadyPassed = true;
                } else
                {
                    Debug.Log("Doubled players??");
                }
            } else
            {
                player.GetComponent<InputManager_HotPotato>().SetHasPotato(false);
            }
        }
    }


    Potato GetPotato()
    {
        return gameObject.GetComponentInChildren<Potato>();
    }
}
