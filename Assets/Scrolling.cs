using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrolling : MonoBehaviour
{
	GameObject firstguy;
	GameObject secondguy;
    float mainxPos;
    float mainyPos;
	float oldxPos;
    float oldyPos;
	float cameraX;
	float cameraY;

    // Start is called before the first frame update
    void Start()
    {
        firstguy = GameObject.Find("mettaur");
        secondguy = GameObject.Find("mettaur (1)");
		cameraX = this.transform.position.x;
		cameraY = this.transform.position.y;
		if(((MonoBehaviour)firstguy.GetComponent("NewBehaviourScript")).enabled){
			oldxPos = firstguy.transform.position.x;
			oldyPos = firstguy.transform.position.y;
		}else{
			oldxPos = secondguy.transform.position.x;
			oldyPos = secondguy.transform.position.y;
		}
    }

    // Update is called once per frame
    void Update()
    {       
		if(((MonoBehaviour)firstguy.GetComponent("NewBehaviourScript")).enabled){
			mainxPos = firstguy.transform.position.x;
			mainyPos = firstguy.transform.position.y;
		}else{
			mainxPos = secondguy.transform.position.x;
			mainyPos = secondguy.transform.position.y;
		}
			
		if(Mathf.Abs(cameraX - mainxPos) > 3){
			cameraX = cameraX - mainxPos < 0 ? cameraX + Mathf.Abs(oldxPos - mainxPos) : cameraX - Mathf.Abs(oldxPos - mainxPos);
			this.transform.position = new Vector3(cameraX, cameraY, -10);
		}
		if(Mathf.Abs(cameraY - mainyPos) > 3){
			cameraY = cameraY - mainyPos < 0 ? cameraY + Mathf.Abs(oldyPos - mainyPos) : cameraY - Mathf.Abs(oldyPos - mainyPos);
			this.transform.position = new Vector3(cameraX, cameraY, -10);
		}
		
		
		if(((MonoBehaviour)firstguy.GetComponent("NewBehaviourScript")).enabled){
			oldxPos = firstguy.transform.position.x;
			oldyPos = firstguy.transform.position.y;
		}else{
			oldxPos = secondguy.transform.position.x;
			oldyPos = secondguy.transform.position.y;
		}
		
    }
}
