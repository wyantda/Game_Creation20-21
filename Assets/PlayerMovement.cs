using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    PlayerControls controls; // holds reference the PlayerControls script
    float moveX; // holds a value from -1 to 1 for the x-axis
    float moveY; // holds a value from -1 to 1 for the y-axis

    public float speed; // value will be multiplied by moveX and moveY to determine movement speed

    bool stunned;
    public bool Stunned
    {
        get { return stunned; }
        set { stunned = value; }
    }

    float stunTimer;
    public float StunTimer
    {
        get { return stunTimer; }
        set { stunTimer = value; }
    }

    Rigidbody2D rb; // reference to the player's Rigidbody2D component

    private void Awake() //called before Start()
    {
        controls = new PlayerControls();

        controls.Land.MoveX.performed += ctx => moveX = ctx.ReadValue<float>();
        controls.Land.MoveX.canceled += ctx => moveX = 0;

        controls.Land.MoveY.performed += ctx => moveY = ctx.ReadValue<float>();
        controls.Land.MoveY.canceled += ctx => moveY = 0;

        controls.Land.Swap.performed += ctx => Swap();

    }

//===============================================================================================================================
    private void OnEnable() //called when the script is enabled (TAMPER WITH CAUTION)
    {
        controls.Land.Enable();

    }

    private void OnDisable() //called when the script is disabled (TAMPER WITH CAUTION)
    {
        controls.Land.Disable();

        rb.velocity = new Vector2(0, 0); // stops the jitter bug
        GetComponent<FollowPlayer>().enabled = true;
    }

//===============================================================================================================================

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    void Update()
    {
        if (!stunned)
        {
            rb.velocity = new Vector2(moveX * speed, moveY * speed);
        }
        else
        {
            stunTimer -= Time.deltaTime;
            if (stunTimer <= 0)
            {
                stunTimer = 0;
                stunned = false;
            }
            else
            {
                rb.velocity = rb.velocity;
            }
            
        }
    }

    void Swap()
    {
        if (GameState.knightLead) // if the knight is the leader
        {
            GameState.knightLead = false;
        }
        else // if the dragon is the leader
        {
            GameState.knightLead = true;  
        }
        GetComponent<PlayerMovement>().enabled = false;
    }
}
