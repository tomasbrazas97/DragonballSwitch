﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleScript : MonoBehaviour
{
    private LevelManager gameLevelManager;
    public int dragonBallValue = 1;

    // Start is called before the first frame update
    void Start()  {
        gameLevelManager = FindObjectOfType<LevelManager>();
    }

    // Update is called once per frame
    void Update() {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") { //only objects with Player tag can destroy object
            FindObjectOfType<SoundsScript>().Play("collect");
            gameLevelManager.AddDragonBall(dragonBallValue); //call addDragonBall from LevelManager and pass int value to it
            Destroy(gameObject);
        }
    }
}
