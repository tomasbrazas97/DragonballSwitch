﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile: MonoBehaviour
{
    private float speed = 4;
    private float direction;
    Rigidbody2D rb;

    private Transform player;
    private Vector2 target;

    // Start is called before the first frame update
    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player").transform;

        target = new Vector2(player.position.x, player.position.y);
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        Enemy.enemyShoot = true;

        if (transform.position.x == target.x && transform.position.y == target.y)
        {
            
            DestroyProjectile();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Triggers Shoot animation
        
        //Triggers sound on projectile shot
        FindObjectOfType<SoundsScript>().Play("Ki");
        if (other.CompareTag("Player"))
        {
            DestroyProjectile();
            
        }
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
       
    }
}

