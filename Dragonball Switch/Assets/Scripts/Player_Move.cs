using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
    //variables
    public float speed = 5f;
    public float jumpSpeed = 6f;
    private float movement = 0f;
    private Rigidbody2D rigidBody;

    public Transform groundCheckPoint; //Bottom of player, checking if theyre on the ground
    public float groundCheckRadius; // Radius of Player ground check
    public LayerMask groundLayer; // jump off things added to this layer
    private bool isTouchingGround;

    private Animator playerAnimation;
    public Vector3 respawnPoint; //Store position of where player is going to respawn to

    private void Start()  {
        //Player assets
        rigidBody = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponent<Animator>();

        //When game loads, respawn point is set to position of player
        respawnPoint = transform.position;
    }

    private void Update() {
        // if player is touching ground = true, if not = false
        isTouchingGround = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, groundLayer);
        movement = Input.GetAxis("Horizontal");

        //will only move if keys left or right are pressed
        if (movement > 0f)  {  //right
            rigidBody.velocity = new Vector2(movement * speed, rigidBody.velocity.y);
            transform.localScale = new Vector2(1f, 1f);
        }
        else if (movement < 0f)  { //left
            rigidBody.velocity = new Vector2(movement * speed, rigidBody.velocity.y);
            transform.localScale = new Vector2(-1f, 1f);
        }
        else { //dont move if none of keys are pressed
            rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
        }

        //Jump, can check all inputs in Edit->Project Settings->Input
        if(Input.GetButtonDown ("Jump") && isTouchingGround == true)  {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpSpeed);
        }

        //Math abs will turn any negative number into positive number
        //When player turns right vel is +, when turns left vel is -
        //RUN ANIMATION WON'T PLAY IF SPEED IS LESS THAN 0.1
        playerAnimation.SetFloat("Speed", Mathf.Abs (rigidBody.velocity.x));
        playerAnimation.SetBool("OnGround", isTouchingGround);
    }

    private void OnTriggerEnter2D(Collider2D other)  {
        if(other.tag == "FallDetector")  {
            //Player enters FallDetector zone (collider with a trigger)
            transform.position = respawnPoint;
        }
        if(other.tag == "Checkpoint") {
            //When player reaches checkpoint, respawn point will be set to position of checkpoint
            respawnPoint = other.transform.position;
        }
    }
}
