using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{

    CircleCollider2D radar; // the range around this enemy that must be entered for it to become hostile 
    BoxCollider2D hitBox; // enemy hitbox

    Rigidbody2D rb; // reference to this enemy's RigidBody component

    Transform trans; // reference to this enemy's Transform component
    Transform currentTarget; // refers to the Transform component enemy's current target

    Vector2 returnPos;

    public float speed;
    public float knockBackPower;
    Vector2 knockBackVector;

    public float attackCooldown;
    public float attackCooldownTimer;

    float hitStunTime;

    public bool chasing;
    public bool attackSuccess;
    public bool blocked;

    PlayerMovement playerMovement;

    void Start()
    {
        radar = GetComponent<CircleCollider2D>();
        hitBox = GetComponent<BoxCollider2D>();

        rb = GetComponent<Rigidbody2D>();

        trans = GetComponent<Transform>();

        returnPos = trans.position;

        attackCooldownTimer = attackCooldown;
        hitStunTime = attackCooldown;

    }

    void Update()
    {
        if (!chasing)
        {
            trans.position = Vector2.MoveTowards(trans.position, returnPos, Time.deltaTime * speed * 0.6f);
            rb.velocity = Vector2.zero;
        }
        else
        {
            Chase(currentTarget.position);
        }
    }

    void Chase(Vector2 target)
    {
        if (!attackSuccess)
        {
            /**
            if (currentTarget.position.x > trans.position.x)
            {
                rb.velocity = new Vector2(speed, rb.velocity.y);
            }
            else if (currentTarget.position.x < trans.position.x)
            {
                rb.velocity = new Vector2(-speed, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }

            if (currentTarget.position.y > trans.position.y)
            {
                rb.velocity = new Vector2(rb.velocity.x, speed);
            }
            else if (currentTarget.position.y < trans.position.y)
            {
                rb.velocity = new Vector2(rb.velocity.x, -speed);
            }
            else
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
            }
            **/

            knockBackVector = new Vector2(currentTarget.position.x - trans.position.x, currentTarget.position.y - trans.position.y);

            knockBackVector.Normalize();

            rb.velocity = knockBackVector * speed;

            //trans.position = Vector2.MoveTowards(trans.position, target, Time.deltaTime * speed);
        }
        else
        {
            if (!blocked)
            {
                attackCooldownTimer -= Time.deltaTime;
                rb.velocity = Vector2.zero;
                if (attackCooldownTimer <= 0)
                {
                    attackCooldownTimer = attackCooldown;
                    attackSuccess = false;
                }
            }
            else {
                attackCooldownTimer -= Time.deltaTime;
                if (attackCooldownTimer <= 0)
                {
                    attackCooldownTimer = attackCooldown;
                    attackSuccess = false;
                    blocked = false;
                }
            }
        }
        
    }

    void KnockBack(Rigidbody2D target)
    {
        knockBackVector.Normalize();
        target.velocity = knockBackVector * knockBackPower;
    }

    //========TRIGGERS=====================================================================================
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.IsTouching(radar) && col.gameObject.tag == "Player")
        {
            currentTarget = col.gameObject.GetComponent<Transform>();
            chasing = true;
        }
    }
    /**
    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.IsTouching(radar) && col.gameObject.tag == "Player")
        {
            Chase(col.gameObject.GetComponent<Transform>().position);
            //print(Time.deltaTime);
        }
    }
    **/
    private void OnTriggerExit2D(Collider2D col)
    {
        if (!col.IsTouching(radar) && col.gameObject.tag == "Player")
        {
            chasing = false;
            attackCooldownTimer = attackCooldown;
        }
    }

    //========COLLIDERS=====================================================================================

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            attackSuccess = true;

            playerMovement = col.gameObject.GetComponent<PlayerMovement>();
            playerMovement.StunTimer += hitStunTime;
            playerMovement.Stunned = true;

            KnockBack(col.gameObject.GetComponent<Rigidbody2D>());

            col.gameObject.SendMessage("Hit", 10.0);

            print("booped");
        }
    }

}
