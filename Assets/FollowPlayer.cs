using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    
    Transform trans; // holds the reference to the object's transform component
    public Transform Trans
    {
        get { return trans; }
    }

    Transform leaderTrans;
    
    public float followDistance;
    public float followSpeed;

    Rigidbody2D rb;

    void Start()
    {
        trans = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();

    }

//===============================================================================================================================

    private void OnDisable() //called when the script is disabled (TAMPER WITH CAUTION)
    {

        GetComponent<PlayerMovement>().enabled = true;

    }

//===============================================================================================================================

    void Update()
    {

        if (GameState.knightLead) // if the knight is leading
        {
            if (gameObject.name == "Player") // if the knight is still a follower when it should be leading...
            {
                GetComponent<FollowPlayer>().enabled = false; // stop following
            }
            else
            {
                leaderTrans = GameState.knightTrans;
            }
            
        }
        else // if the dragon is leading
        {
            if (gameObject.name == "Dragon") // if the dragon is still a follower when it should be leading...
            {
                GetComponent<FollowPlayer>().enabled = false; // stop following
            }
            else
            {
                leaderTrans = GameState.dragonTrans;
            }
            
        }

        if (Vector2.Distance(trans.position, leaderTrans.position) >= followDistance)
        {
            trans.position = Vector2.MoveTowards(trans.position, leaderTrans.position, followSpeed * Time.deltaTime);
        }
        else
        {
            rb.velocity = new Vector2(0, 0); // don't move
        }

    }
}
