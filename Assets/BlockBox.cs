using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBox : MonoBehaviour
{
    public float blocktimer = 0.3f;
    BoxCollider2D hitBox;
    BasicEnemy enemyscript;

    // Start is called before the first frame update
    void Start()
    {
        hitBox = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        blocktimer -= Time.deltaTime;
        if (blocktimer < 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy" && col.GetType() == typeof(BoxCollider2D))
        {
            KnockBack(col.gameObject.GetComponent<Rigidbody2D>());
            enemyscript = col.gameObject.GetComponent<BasicEnemy>();
            enemyscript.attackSuccess = true;
            enemyscript.attackCooldownTimer = blocktimer;
            enemyscript.blocked = true;
            //Destroy(this.gameObject);
        }
    }

    void KnockBack(Rigidbody2D enemy) {
        enemy.velocity = new Vector2(0,5);
    }
}
