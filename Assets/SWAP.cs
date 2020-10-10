using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SWAP : MonoBehaviour
{
	MonoBehaviour followScript;
	bool script1 = false;
	bool script2 = false;
	GameObject swapwith;
	Vector2 oldPos;
	Vector2 oldSwapPos;
	int counter = 0;
    // Start is called before the first frame update
    void Start()
    {
        followScript = ((MonoBehaviour)gameObject.GetComponent("followingSwap")) != null ? ((MonoBehaviour)gameObject.GetComponent("followingSwap")) : ((MonoBehaviour)gameObject.GetComponent("following"));
		swapwith = ((MonoBehaviour)gameObject.GetComponent("followingSwap")) != null ? GameObject.Find("mettaur (1)") : GameObject.Find("mettaur");
	}

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z) && counter == 0){
				script1 = !followScript.enabled;
				script2 = !((MonoBehaviour)gameObject.GetComponent("NewBehaviourScript")).enabled;
				followScript.enabled = false;
				((MonoBehaviour)gameObject.GetComponent("NewBehaviourScript")).enabled = false;
				oldPos = new Vector2 (this.transform.position.x, this.transform.position.y);
				oldSwapPos = new Vector2 (swapwith.transform.position.x, swapwith.transform.position.y);
				++counter;
			}
		if(counter > 0){
			if(script1 == false){
					this.transform.position = new Vector2(this.transform.position.x + (oldSwapPos.x - this.transform.position.x)/2, this.transform.position.y + (oldSwapPos.y - this.transform.position.y)/2);
					swapwith.transform.position = new Vector2(swapwith.transform.position.x + (oldPos.x - swapwith.transform.position.x)/2, swapwith.transform.position.y + (oldPos.y - swapwith.transform.position.y)/2);
				}
			++counter;
			if(counter > 3){
				followScript.enabled = script1;
				((MonoBehaviour)gameObject.GetComponent("NewBehaviourScript")).enabled = script2;
				counter = 0;
			}
		}
	}
}
