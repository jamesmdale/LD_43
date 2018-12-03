using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;



public class InputManager_JumpRope : NetworkBehaviour
{
    [SyncVar]
    Vector3 finalScale = new Vector3(1, 1, 1);

    [SyncVar]
    private float timeRemainingHit = 0.0f;

    [SyncVar]
    private int chancesLeft = 3;

    [SyncVar]
    private bool playerIsOutOfMatch = false;

    public float hitDuration = 1.0f;
    public float jumpDuration = 0.2f;
    public float maxScale = 7.0f;

    private float m_playerSpeed = 5.0f;
    private float timeRemainingJump = 0.0f;

    // When this script gets enabled
    private void OnEnable()
    {

    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.localScale = finalScale;

        if (timeRemainingHit > 0.0f)
            GetComponent<SpriteRenderer>().material.color = Color.red;           // Was hit
        else
        {
            if(isLocalPlayer)
                GetComponent<SpriteRenderer>().material.color = Color.blue;          // No hits!
            else
                GetComponent<SpriteRenderer>().material.color = Color.white;          // No hits!
        }
        
        if (!isLocalPlayer)
            return;

        if (playerIsOutOfMatch)
            return;

        CmdDecreamentTimeRemainingHit();

        float x = Input.GetAxis("Horizontal") * Time.deltaTime * m_playerSpeed;
        float y = Input.GetAxis("Vertical") * Time.deltaTime * m_playerSpeed;

        gameObject.transform.Translate(x, y, 0.0f);

        // Start Jumping
        bool spaceJustPressed = Input.GetKeyDown(KeyCode.Space);
        if (spaceJustPressed && (timeRemainingJump <= 0.0f))
        {
            Debug.Log("JUMPIE!");
            timeRemainingJump = jumpDuration;
        }

        // Decrease time remaining in which jump ends
        if (timeRemainingJump > 0.0f)
        {
            timeRemainingJump -= Time.deltaTime;
            float scale = 1.0f;

            if ( timeRemainingJump < jumpDuration * 0.5f )
                scale = RangeMapFloat(timeRemainingJump, 0.0f, jumpDuration, 1.0f, maxScale);
            else
                scale = RangeMapFloat(timeRemainingJump, 0.0f, jumpDuration, maxScale, 1.0f);

            CmdSetFinalScale(scale);
        }

        // If negative, set to zero
        if (timeRemainingJump < 0.0f)
        {
            timeRemainingJump = 0.0f;
            CmdSetFinalScale(1.0f);
        }
    }

    bool IsJumping()
    {
        return timeRemainingJump > 0.0f;
    }

    float RangeMapFloat(float inValue, float inStart, float inEnd, float outStart, float outEnd)
    {
        // If inRange is zero, call of this function is not-appropriate, handle this situation..
        if (inStart == inEnd)
        {
            return (outStart + outEnd) * 0.5f;
        }

        // Function call is appropriate, start calculation
        float inRange = inEnd - inStart;
        float outRange = outEnd - outStart;
        float inRelativeToStart = inValue - inStart;
        float fractionOfRange = inRelativeToStart / inRange;            // inRange can't be ZERO
        float outRelativeToStart = fractionOfRange * outRange;

        return outRelativeToStart + outStart;
    }

    [Command]
    void CmdSetFinalScale( float scale )
    {
        finalScale = new Vector3(scale, scale, finalScale.z);
    }

    [Command]
    void CmdTakeAHit()
    {
        // Shows red color for some time
        timeRemainingHit = hitDuration;

        // Decrements chances
        chancesLeft -= 1;

        // Is this player out of match?
        if( chancesLeft <= 0 )
            playerIsOutOfMatch = true;
    }

    [Command]
    void CmdDecreamentTimeRemainingHit()
    {
        if (timeRemainingHit > 0.0f)
            timeRemainingHit -= Time.deltaTime;
        else
            timeRemainingHit = 0.0f;
    }

    [ClientRpc]
    public void RpcJustCollidedWithHand()
    {
        CmdTakeAHit();
    }
}
