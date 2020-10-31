using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float HP = 100f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Hit(float damage) {
        HP -= damage;
        if (HP <= 0) {
            Gameover();
        }
    }
    void Gameover() {

    }
}
