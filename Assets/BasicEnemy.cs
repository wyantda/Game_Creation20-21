using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    Rigidbody2D body;
    Vector2 spawnPoint;
    public int moverange;
    public int movespeed = 4;
    public int direction;
    private Transform target = null;
    Vector2 movedirs;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        spawnPoint.x = body.transform.position.x;
        spawnPoint.y = body.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            if (Mathf.Abs(this.transform.position.x - target.position.x) > 1)
            {
                movedirs.x = target.position.x - transform.position.x;
            }
            else
            {
                movedirs.x = 0;
            }

            if (Mathf.Abs(this.transform.position.y - target.position.y) > 1)
            {
                movedirs.y = target.position.y - transform.position.y;
            }
            else if (this.transform.position.y > target.position.y)
            {
                movedirs.y = target.position.y - transform.position.y;
            }
            else
            {
                movedirs.y = 0;
            }
            float distance = Mathf.Abs(Vector2.Distance(new Vector2(0, 0), movedirs));
            if (distance != 0)
            {
                movedirs.x /= Mathf.Abs(Vector2.Distance(new Vector2(0, 0), movedirs));
                movedirs.y /= Mathf.Abs(Vector2.Distance(new Vector2(0, 0), movedirs));
            }
        }
        else if (Vector2.Distance(spawnPoint, transform.position) < 1)
        {
            movedirs.x = 0;
            movedirs.y = 0;
        }
        else
        {
            if (Mathf.Abs(transform.position.x - spawnPoint.x) > 1)
            {
                movedirs.x = spawnPoint.x - transform.position.x;
            }
            else
            {
                movedirs.x = 0;
            }

            if (Mathf.Abs(transform.position.y - spawnPoint.y) > 1)
            {
                movedirs.y = spawnPoint.y - transform.position.y;
            }
            else
            {
                movedirs.y = 0;
            }
            float distance = Mathf.Abs(Vector2.Distance(new Vector2(0, 0), movedirs));
            if (distance != 0)
            {
                movedirs.x /= Mathf.Abs(Vector2.Distance(new Vector2(0, 0), movedirs));
                movedirs.y /= Mathf.Abs(Vector2.Distance(new Vector2(0, 0), movedirs));
            }

        }

        body.velocity = new Vector2(movedirs.x * movespeed, movedirs.y * movespeed);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            target = other.transform;
            Debug.Log("In Range");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            target = null;
            Debug.Log("Out of Range");
        }
    }
}