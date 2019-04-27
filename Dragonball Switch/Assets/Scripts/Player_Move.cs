using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
    //variables
    public float speed = 0f;
    public float jumpSpeed = 6f;
    private float movement = 0f;
    private float direction = 1f; //instantiated to allow projectiles to work
    private Rigidbody2D rigidBody;
    private bool attack;
    public GameObject attackTrigger;
    private bool combo;
    public Transform firePoint;
    public LineRenderer lineRender;

    public Transform groundCheckPoint; //Bottom of player, checking if theyre on the ground
    public float groundCheckRadius; // Radius of Player ground check
    public LayerMask groundLayer; // jump off things added to this layer
    private bool isTouchingGround;

    private Animator playerAnimation;
    public Vector3 respawnPoint; //Store position of where player is going to respawn to
    public LevelManager gameLevelManager;

   

    public GameObject Fireball;

    [SerializeField]
    private Stat health;
    private float waitTime =3f;

    [SerializeField]
    private Stat energy;

    private void Awake()
    {
        health.Initialize();
        energy.Initialize();
    }

    private void Start()  {
        //Player assets
        rigidBody = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponent<Animator>();

        //When game loads, respawn point is set to position of player
        respawnPoint = transform.position;

        gameLevelManager = FindObjectOfType<LevelManager>();
      
       
    }

    private void Update() {
        // if player is touching ground = true, if not = false
        isTouchingGround = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, groundLayer);
        movement = Input.GetAxis("Horizontal");

        if (!this.playerAnimation.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            //will only move if keys left or right are pressed
            if (movement > 0f)
            {  //right
                direction =1f;
                rigidBody.velocity = new Vector2(movement * speed, rigidBody.velocity.y);
                transform.localScale = new Vector2(1f, 1f);
            }
            else if (movement < 0f)
            { //left
                direction = -1f;
                rigidBody.velocity = new Vector2(movement * speed, rigidBody.velocity.y);
                transform.localScale = new Vector2(-1f, 1f);
            }
            else
            { //dont move if none of keys are pressed
                rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
            }

            //Jump, can check all inputs in Edit->Project Settings->Input
            if (Input.GetButtonDown("Jump") && isTouchingGround == true)
            {
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpSpeed);
            }
        }

        //Math abs will turn any negative number into positive number
        //When player turns right vel is +, when turns left vel is -
        //RUN ANIMATION WON'T PLAY IF SPEED IS LESS THAN 0.1
        playerAnimation.SetFloat("Speed", Mathf.Abs (rigidBody.velocity.x));
        playerAnimation.SetBool("OnGround", isTouchingGround);

        //Attack 
        if (Input.GetKeyDown(KeyCode.Z)) {//Attack input
            attack = true;
            rigidBody.velocity = Vector2.zero;
            attackTrigger.SetActive(true);
            playerAnimation.SetTrigger("Attack");

            //Detects which character is displayed and plays their respective voice clip
            if (SwitchScript.charDisplayed == 1)
            {
                FindObjectOfType<SoundsScript>().Play("Atk");
                FindObjectOfType<SoundsScript>().Play("GokuAtk1");
            }
            else
            {
                FindObjectOfType<SoundsScript>().Play("Atk");
                FindObjectOfType<SoundsScript>().Play("VegAtk1");
            }
        }
        if (attack && Input.GetKeyDown(KeyCode.Z) && !playerAnimation.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            attackTrigger.SetActive(false);

        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            playerAnimation.SetTrigger("Shoot");
            ShootProjectile(0);
            if (SwitchScript.charDisplayed == 1)
            {
                FindObjectOfType<SoundsScript>().Play("Ki");
                FindObjectOfType<SoundsScript>().Play("GokuAtk3");
            }
            else
            {
                FindObjectOfType<SoundsScript>().Play("Ki");
                FindObjectOfType<SoundsScript>().Play("VegAtk4");
            }
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            rigidBody.velocity = Vector2.zero;

            if (SwitchScript.charDisplayed == 1)
            
            {
                StartCoroutine(SpecialAtk());
                playerAnimation.SetTrigger("Special");
                FindObjectOfType<SoundsScript>().Play("GokuSpecial");
                FindObjectOfType<SoundsScript>().Play("GokuSpec");
            }
            else
            {
                StartCoroutine(SpecialAtk());
                playerAnimation.SetTrigger("Special");
                FindObjectOfType<SoundsScript>().Play("vegetaSpecial");
                FindObjectOfType<SoundsScript>().Play("vegSpecial");
            }
        }
        //Attack 
        if (Input.GetKeyDown(KeyCode.C))
        {
            StartCoroutine(Combo());
        }

        }

    private void OnTriggerEnter2D(Collider2D other)  {
        if(other.tag == "FallDetector")  {
            //Player enters FallDetector zone (collider with a trigger)
            gameLevelManager.Respawn(); // calling respawn method from LevelManager script
            FindObjectOfType<SoundsScript>().Play("Respawn");
        }
        if(other.tag == "Checkpoint") {
            //When player reaches checkpoint, respawn point will be set to position of checkpoint
            FindObjectOfType<SoundsScript>().Play("Respawn");
            respawnPoint = other.transform.position;
        }

        if(other.tag == "Spikes")
        {
            health.CurrentVal -= 10;
            if (SwitchScript.charDisplayed == 1)
            {
                FindObjectOfType<SoundsScript>().Play("GokuHurt");
            }
            else
            {
                FindObjectOfType<SoundsScript>().Play("VegHurt");
            }
        }
    

        if(other.tag == "Projectile")
        {
            health.CurrentVal -= 5;
            if (SwitchScript.charDisplayed == 1)
            {
                FindObjectOfType<SoundsScript>().Play("GokuHurt");
            }
            else
            {
                FindObjectOfType<SoundsScript>().Play("VegHurt");
            }
        }
    }

    //Shoot projectile Fireball
    public void ShootProjectile(int value)
    {
        if (direction > 0f)
        { 
            GameObject tmp = (GameObject)Instantiate(Fireball, transform.position, Quaternion.identity);
            tmp.GetComponent<ProjectileScript>().Initialize(Vector2.right);
        }//right facing
        else if (direction <0f)
        {
            GameObject tmp = (GameObject)Instantiate(Fireball, transform.position, Quaternion.identity);
            tmp.GetComponent<ProjectileScript>().Initialize(Vector2.left);
        }//left facing
    }
        
    IEnumerator SpecialAtk()
    {
        yield return new WaitForSeconds(1);
        RaycastHit2D hitInfo = Physics2D.Raycast(firePoint.position, firePoint.right);
        if (hitInfo)
        {
            Enemy enemy = hitInfo.transform.GetComponent<Enemy>();

            lineRender.SetPosition(0, firePoint.position);
            lineRender.SetPosition(1, hitInfo.point);


        }
        else
        {
            lineRender.SetPosition(0, firePoint.position);
            lineRender.SetPosition(1, firePoint.position + firePoint.right * 10);
        }


        lineRender.enabled = true;
        //Wait until end of animation
        yield return new WaitForSeconds(1);
        lineRender.enabled = false;
    }
    IEnumerator Combo()
    {
       
            combo = true;
            rigidBody.velocity = Vector2.zero;
            attackTrigger.SetActive(true);
            playerAnimation.SetTrigger("Attack");
      
            playerAnimation.SetTrigger("Combo");
 

        //Detects which character is displayed and plays their respective voice clip
        if (SwitchScript.charDisplayed == 1)
            {
                FindObjectOfType<SoundsScript>().Play("Atk");
                FindObjectOfType<SoundsScript>().Play("GokuAtk1");
                yield return new WaitForSeconds(1);
                FindObjectOfType<SoundsScript>().Play("Atk");
                FindObjectOfType<SoundsScript>().Play("GokuAtk2");
                yield return new WaitForSeconds(1);
                FindObjectOfType<SoundsScript>().Play("Atk");
                FindObjectOfType<SoundsScript>().Play("GokuAtk3");
                yield return new WaitForSeconds(1);
            }
            else
            {
                FindObjectOfType<SoundsScript>().Play("Atk");
                FindObjectOfType<SoundsScript>().Play("VegAtk3");
                yield return new WaitForSeconds(0);
                FindObjectOfType<SoundsScript>().Play("Atk");
                FindObjectOfType<SoundsScript>().Play("VegAtk1");
                yield return new WaitForSeconds(1);
                FindObjectOfType<SoundsScript>().Play("Atk");
                FindObjectOfType<SoundsScript>().Play("VegAtk2");
        }
        
        if (combo && Input.GetKeyDown(KeyCode.Z) && !playerAnimation.GetCurrentAnimatorStateInfo(0).IsTag("Combo"))
        {
            attackTrigger.SetActive(false);

        }

    }
}
