﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public float respawnDelay = 1;
    public Player_Move gamePlayer; //referring to player_move script and object it is attached to
    public int dragonBall;
    public Text scoreText;
    public Transform pfHpBar; 

    // Start is called before the first frame update
    void Start()
    {
        gamePlayer = FindObjectOfType<Player_Move>();
        scoreText.text = "Score: " + dragonBall;
        
        //Transform healthBarTranform = Instantiate(pfHpBar, new Vector3(0, 10), Quaternion.identity); //Instantiate the HP Bar
        //HpBarScript hpBar = healthBarTranform.GetComponent<HpBarScript>();
        //hpBar.Setup(healthSystem);
       // healthSystem.Damage(10);
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void Respawn()  {
        StartCoroutine("RespawnCoroutine");
    }

    public IEnumerator RespawnCoroutine()
    {   
        gamePlayer.gameObject.SetActive(false); //disabling player object temp
        yield return new WaitForSeconds(respawnDelay);
        gameObject.gameObject.SetActive(true); //re-enabling player object
        gamePlayer.transform.position = gamePlayer.respawnPoint; //setting position point to respawn point in player_move script
        
    }

    public void AddDragonBall(int numberOfDragonBalls) { //adds coin to the game depending on coin value
        dragonBall += numberOfDragonBalls;
        scoreText.text = "Score: " + dragonBall;
    }
}
