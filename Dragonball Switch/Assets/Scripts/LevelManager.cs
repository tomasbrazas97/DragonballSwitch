﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public float respawnDelay; // amount of time to wait before respawn
    public Player_Move gamePlayer; //referring to player_move script and object it is attached to


    // Start is called before the first frame update
    void Start()
    {
        gamePlayer = FindObjectOfType<Player_Move>();
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void Respawn()  {
        gamePlayer.gameObject.SetActive(false); //disabling player object temp
        gamePlayer.transform.position = gamePlayer.respawnPoint; //setting position point to respawn point in player_move script
        gameObject.gameObject.SetActive(true); //re-enabling player object
    }
}
