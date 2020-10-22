using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDragon : MonoBehaviour
{
	MonoBehaviour followScript;
	Vector2 currentPos;
	
    public float attackTime;
    public float startTimeAttack;

    public Transform attackLocation;
    public float attackRange;
    public LayerMask enemies;
	
    // Start is called before the first frame update
    void Start()
    {
        followScript = ((MonoBehaviour)gameObject.GetComponent("followingSwap")) != null ? ((MonoBehaviour)gameObject.GetComponent("followingSwap")) : ((MonoBehaviour)gameObject.GetComponent("following"));
    }

    // Update is called once per frame
    void Update()
    {
		if(!followScript.enabled){
			if(attackTime <= 0){	
				attackLocation.transform.position = new Vector2(this.transform.position.x, this.transform.position.y);
				if(Input.GetKeyDown(KeyCode.X)){
					attackTime = startTimeAttack;
					currentPos = new Vector2(this.transform.position.x, this.transform.position.y);
				}
			}else{
				attackTime -= 0.05f;
				this.transform.position = currentPos;
				
				if(GameObject.Find("mettaur (1)").GetComponent<NewBehaviourScript>().direction == 1){ //facing RIGHT
					attackLocation.transform.position = new Vector2(attackLocation.transform.position.x + 1, attackLocation.transform.position.y);
				}else if(GameObject.Find("mettaur (1)").GetComponent<NewBehaviourScript>().direction == 2){ //facing LEFT
					attackLocation.transform.position = new Vector2(attackLocation.transform.position.x - 1, attackLocation.transform.position.y);
				}else if(GameObject.Find("mettaur (1)").GetComponent<NewBehaviourScript>().direction == 10){ //facing UP
					attackLocation.transform.position = new Vector2(attackLocation.transform.position.x, attackLocation.transform.position.y + 1);
				}else if(GameObject.Find("mettaur (1)").GetComponent<NewBehaviourScript>().direction == 20){ //facing DOWN
					attackLocation.transform.position = new Vector2(attackLocation.transform.position.x, attackLocation.transform.position.y - 1);
				}
					
				Collider2D[] damage = Physics2D.OverlapCircleAll(attackLocation.position, attackRange, enemies);
					for (int i = 0; i < damage.Length; i++)
					{
						Destroy(damage[i].gameObject);
					}
			}
		}
    }
	
	private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackLocation.position, attackRange);
    }
    
}
