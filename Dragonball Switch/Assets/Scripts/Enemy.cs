﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float stoppingDistance;
    public float retreatDistance;
    private Animator enemyAnimation;
    private bool isDead;
    private float direction;
    public static bool enemyShoot = false;

    private bool facingRight;

    private float timeBtwShots;
    public float startTimeBtwShots;

    public GameObject projectile;
    private Transform player;

    [SerializeField]
    private Stat health;

    private void Awake()
    {
        health.Initialize();
    }


    // Start is called before the first frame update
    void Start()
    {
        //Player tag
        player = GameObject.FindGameObjectWithTag("Player").transform;

        enemyAnimation = GetComponent<Animator>();

        
        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        Flip(horizontal);
        if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            timeBtwShots = startTimeBtwShots;
          
        }
       else if (Vector2.Distance(transform.position,player.position) <= stoppingDistance && Vector2.Distance(transform.position,player.position)>retreatDistance)
        {
            transform.position = this.transform.position;
          

        }
        else if (Vector2.Distance(transform.position, player.position) > retreatDistance)
        {
            transform.localScale = new Vector2(-1f, 1f);
            transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
            timeBtwShots = 100;
            enemyAnimation.SetTrigger("Attack");
        }

        if (enemyShoot == true)
        {
            //Stops animation Loop
            enemyShoot = false;
            enemyAnimation.SetTrigger("Shoot");
        

        }

        if (timeBtwShots <= 0)
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
            timeBtwShots = startTimeBtwShots;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }


        if (health.CurrentVal == 0)
        {
       
            timeBtwShots = 100;
            FindObjectOfType<SoundsScript>().Play("SaibaDeath");
            isDead = true;
            enemyAnimation.SetBool("Dead", isDead);
            Destroy(gameObject, 2f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "ProjectileEnem")
        {
            health.CurrentVal -= 50;
            enemyAnimation.SetTrigger("Hurt");
        }

        if (other.tag == "Melee")
        {
            health.CurrentVal -= 20;
            enemyAnimation.SetTrigger("Hurt");
        }
        if (other.tag == "Special")
        {
            health.CurrentVal -= 90;
            enemyAnimation.SetTrigger("Hurt");
        }

    }

    //Disables enemeies when off screen
    private void OnBecameInvisible()
    {
        GetComponent <Enemy> ().enabled = false;
    }
    //Re enables enemies once they are visible
    private void OnBecameVisible()
    {
        GetComponent<Enemy>().enabled = true;
    }

    private void Flip (float horizontal)
    {
        if(horizontal < 0 && !facingRight || horizontal > 0 && facingRight)
        {
            facingRight = !facingRight;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }
}
