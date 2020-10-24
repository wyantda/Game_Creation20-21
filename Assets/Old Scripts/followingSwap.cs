using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followingSwap : MonoBehaviour
{
    Rigidbody2D body;
    float mainxPos;
    float targetxPos;
    float mainyPos;
    float targetyPos;
    public float movex;
    public float movey;
    int dir = 0;
    float movelimiter = 0.65f;
    public Vector2 dirMult;
    public Vector2 speed = new Vector2(15, 15);
    public Vector2 truedist;

    public float direction_distance = 1;
    public float follow_distance = 1;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        mainxPos = GameObject.Find("mettaur (1)").transform.position.x;
        mainyPos = GameObject.Find("mettaur (1)").transform.position.y;
        dir = GameObject.Find("mettaur (1)").GetComponent<NewBehaviourScript>().direction;
        switch (dir % 10) {
            case 1:
                dirMult.x = 1;
                break;
            case 2:
                dirMult.x = -1;
                break;
            default:
                dirMult.x = 0;
                break;
        }
        switch (dir / 10) {
            case 1:
                dirMult.y = 1;
                break;
            case 2:
                dirMult.y = -1;
                break;
            default:
                dirMult.y = 0;
                break;
        }
        
        if (dirMult.x == 0)
        {
            truedist.y = follow_distance;
            truedist.x = 0.2f;
        }
        else if (dirMult.y == 0)
        {
            truedist.y = 0.2f;
            truedist.x = follow_distance;
        }
        else {
            truedist.x = follow_distance * .85f;
            truedist.y = follow_distance * .85f;
        }

        targetxPos = mainxPos + truedist.x * dirMult.x * -1;
        targetyPos = mainyPos + truedist.y * dirMult.y * -1;

        if (Mathf.Abs(targetxPos - this.transform.position.x) > truedist.x) {
            if (targetxPos > this.transform.position.x) {
                movex = 1;
            } else if (targetxPos < this.transform.position.x)
            {
                movex = -1;
            }
        } else {
            movex = 0;
        }

        if (Mathf.Abs(targetyPos - this.transform.position.y) > truedist.y)
        {
            if (targetyPos > this.transform.position.y)
            {
                movey = 1;
            }
            else if (targetyPos < this.transform.position.y)
            {
                movey = -1;
            }
        }
        else{
            movey = 0;
        }

        if (Mathf.Abs(movey) != 0 && Mathf.Abs(movex) != 0) {
            movex *= movelimiter;
            movey *= movelimiter;
        }

        body.velocity = new Vector2(movex * speed.x, movey * speed.y);
    }
}
