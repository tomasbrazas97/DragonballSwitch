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

        timeBtwShots = startTimeBtwShots;
        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
        else if (Vector2.Distance(transform.position, player.position) < stoppingDistance &&
            Vector2.Distance(transform.position, player.position) > retreatDistance)
        {
            transform.position = this.transform.position;
        }
        else if (Vector2.Distance(transform.position, player.position) < retreatDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
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


        if(health.CurrentVal == 0)
        {
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
        }
    }
}
