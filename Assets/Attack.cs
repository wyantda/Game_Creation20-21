using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
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
				if(Input.GetKeyDown(KeyCode.X)){
					Collider2D[] damage = Physics2D.OverlapCircleAll(attackLocation.position, attackRange, enemies);
					for (int i = 0; i < damage.Length; i++)
					{
						Destroy(damage[i].gameObject);
					}
					attackTime = startTimeAttack;
					currentPos = new Vector2(this.transform.position.x, this.transform.position.y);
				}
			}else{
				attackTime -= 0.05f;
				this.transform.position = currentPos;
			}
		}
    }
	
	private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackLocation.position, attackRange);
    }
    
}
