using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;

public class GameState : MonoBehaviour
{

    public static bool knightLead; // is the knight leading?
    public static Transform knightTrans; // reference to the knight's Transform component
    public static Transform dragonTrans; // reference to the dragon's Transform component

    void Awake()
    {

        knightLead = true;
        knightTrans = GameObject.Find("Player").GetComponent<Transform>();
        dragonTrans = GameObject.Find("Dragon").GetComponent<Transform>();

        Vector2 vect = new Vector2(0, 0);
        print(vect);
        vect.Normalize();
        print(vect);
    }

    void Update()
    {
        
        
    }
}
