using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float moveSpeed;
	public float jumpForce;
	private Rigidbody2D myRigidBody;
	public bool grounded;
	public LayerMask whatIsGround;
	public LayerMask whatIsEnemy;
    public LayerMask whatIsBoat;
    public LayerMask whatIsWater;
    public Renderer thisSheet;
	public bool canGameOver;
	public bool gameOver;
	public bool isWater;
	private BoxCollider2D myCollider;
    private PolygonCollider2D enemyDetector;
    private Animator myAnimator;
	private bool airJump;
	public AudioSource audioData;
	public AudioClip jumpSound,trashCan, shoot, deadWater, deadEnemy;
	public  bool hasBucket;
	public GameObject Spear;
	private GameObject SpearBody;
	public GameObject shooteffect;
	public GameObject dead;
    public GameObject deadWaterEffect;
    public GameObject deadpos;
	public GameObject shootpos;
	public float spearLifeSpan;
    public GameObject feet;
    public bool victory;
    public bool shovel;
    public float fallspeed;
    private int counter;
    public int maxCount;
	private bool deadeffect;
    private bool deadfromwater;
    private bool jump;
    void Start()
    {
        jump = false;
        deadfromwater = false;
        deadeffect =false;
        if (fallspeed == null) fallspeed = moveSpeed;
        shovel = false;
    SpearBody =null;
    myRigidBody = GetComponent<Rigidbody2D>();
        enemyDetector = GetComponent<PolygonCollider2D>();
	audioData = GetComponent<AudioSource>();
	myCollider = feet.GetComponent<BoxCollider2D>();
	myAnimator = GetComponent<Animator>();
	gameOver=false;
	airJump=false;
        victory = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (Physics2D.IsTouchingLayers(myCollider, whatIsBoat)&&!jump)
        {
            myRigidBody.velocity = new Vector2(moveSpeed / 2, 0);
        }
        else
        {
            if (SpearBody != null)
            {
                SpearBody.transform.position = new Vector3(shootpos.transform.position.x + 8, SpearBody.transform.position.y, SpearBody.transform.position.z);
            }
            grounded = Physics2D.IsTouchingLayers(myCollider, whatIsGround);
            if (!shovel)
            {
                myRigidBody.velocity = new Vector2(moveSpeed, myRigidBody.velocity.y);
            }
            else
            {
                {

                    myRigidBody.velocity = new Vector2(0, fallspeed);
                    if (counter < maxCount) counter++;
                    else { counter = 0; shovel = false;
                        myAnimator.SetBool("shovel", false);
                    }
                }
            }
                if (gameOver)
                {
                    if (!deadeffect)
                    {
                        if (!deadfromwater) Instantiate(dead, deadpos.transform.position, transform.rotation);
                        if (deadfromwater) Instantiate(deadWaterEffect, deadpos.transform.position, transform.rotation);
                        deadeffect = true;
                    }
                    thisSheet.enabled = false;
                    myRigidBody.velocity = new Vector2(0, 0);
                    return;
                }
                if (Physics2D.IsTouchingLayers(enemyDetector, whatIsEnemy) && canGameOver)
                {
                    audioData.PlayOneShot(deadEnemy, 1);
                    gameOver = true;
                }
                if (Physics2D.IsTouchingLayers(enemyDetector, whatIsWater) && canGameOver)
                {
                    deadfromwater = true;
                    audioData.PlayOneShot(deadWater, 1);
                    gameOver = true;
                }


            }

            myAnimator.SetFloat("Speed", myRigidBody.velocity.x);
            if (!isWater)
            {
                myAnimator.SetBool("Grounded", grounded);
                myAnimator.SetBool("gameOver", gameOver);
            }
            if (Input.GetKeyDown("space"))
            {
                NormalJump();
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                spearThrow();
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                Shovel();
            }
        }
    
        
    
	public void DisablePic(){
		thisSheet.enabled=false;
        victory = true;
	}

	public void spearThrow(){
			if(SpearBody==null&&hasBucket){
			Instantiate(shooteffect, shootpos.transform.position, transform.rotation);
            SpearBody = Instantiate(Spear, new Vector3(shootpos.transform.position.x+8, shootpos.transform.position.y, shootpos.transform.position.z), shootpos.transform.rotation);
			Destroy(SpearBody,spearLifeSpan);
            audioData.PlayOneShot(shoot, 1);
        }
	}
    public void Shovel()
    {
        grounded = Physics2D.IsTouchingLayers(myCollider, whatIsGround);
        if (!shovel&& hasBucket&&!grounded) shovel = true;
        myAnimator.SetBool("shovel", true);

    }
    public void NormalJump(){
        
		grounded = Physics2D.IsTouchingLayers(myCollider, whatIsGround);
        if (Physics2D.IsTouchingLayers(myCollider, whatIsBoat)) jump = true;
           
        if (!shovel)
        {
            if (grounded || isWater)
            {
                myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, jumpForce);
                airJump = true;
                audioData.PlayOneShot(jumpSound, 1);
            }
            if (airJump && !grounded)
            {
                myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, jumpForce);
                airJump = false;
                audioData.PlayOneShot(jumpSound, 1);

            }
        }
	}
	
	public bool PickUp(bool isBucket){

		if(isBucket){
	
            if (!hasBucket)
            {
                audioData.PlayOneShot(trashCan);
            }
            hasBucket = true;
            myAnimator.SetBool ("hasBucket", true);
		
		return false;
		}
		
		else if(hasBucket&&!isBucket){
			if (!audioData.isPlaying)
        {
		    audioData.PlayOneShot(trashCan);}
			return false;
		}
		return false;
	}
	
}
