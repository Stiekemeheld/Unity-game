using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boatController : MonoBehaviour
{
	
	public LayerMask whatIsKoelibald;
	private Collider2D myCollider;
    public PlayerController thePlayer;
    private Rigidbody2D myRigidBody;

    void Start()
    {
        thePlayer = FindObjectOfType<PlayerController>();
        myCollider = GetComponent<Collider2D>();
        myRigidBody = GetComponent<Rigidbody2D>();
    }
		void Update(){
			if(Physics2D.IsTouchingLayers(myCollider, whatIsKoelibald)){
            myRigidBody.velocity = new Vector2(thePlayer.moveSpeed/2, 0);
		}
       
		}
	
}