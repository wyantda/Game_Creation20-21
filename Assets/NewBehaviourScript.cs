using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NewBehaviourScript : MonoBehaviour
{
    Rigidbody2D body;
    float inputX;
    float inputY;
    float movelimiter = 0.7f;
    public Vector2 speed = new Vector2(15,15);
    public int direction = -1;
    int lastdir = 0;
    // Start is called before the first frame update
    void Start()
    {
        //pog
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(speed.x * inputX, speed.y * inputY, 0);
        //movement *= Time.deltaTime;

         
    }

    void FixedUpdate()
    {
        if (inputX != 0 && inputY != 0) {
            inputX *= movelimiter;
            inputY *= movelimiter;
        }
        body.velocity = new Vector2(speed.x * inputX, speed.y * inputY);
        //direction: X1X2 where X1 is L/R and x2 is U/D
        if (Mathf.Abs(inputX) > 0.1)
        {
            if (inputX > 0.1)
            {
                direction = 1;
            }
            else
            {
                direction = 2;
            }
        }
        else {
            direction = 0;
        }
        if (Mathf.Abs(inputY) > 0.1)
        {
            if (inputY > 0.1)
            {
                direction += 10;
            }
            else
            {
                direction += 20;
            }
        }
        else
        {
            //direction = 0;
        }
        if (inputX == 0 && inputY == 0)
        {
            if (inputX == 0 && inputY == 0) {
                direction = lastdir;
        }
        }
        lastdir = direction;
    }
}
