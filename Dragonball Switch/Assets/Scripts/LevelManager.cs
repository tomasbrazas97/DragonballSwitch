using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public float respawnDelay;
    public Player_Move gamePlayer; //referring to player_move script and object it is attached to
    public int dragonBall;
    public Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        gamePlayer = FindObjectOfType<Player_Move>();
        scoreText.text = "Score: " + dragonBall;
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
        gamePlayer.transform.position = gamePlayer.respawnPoint; //setting position point to respawn point in player_move script
        gameObject.gameObject.SetActive(true); //re-enabling player object
    }

    public void AddDragonBall(int numberOfDragonBalls) { //adds coin to the game depending on coin value
        dragonBall += numberOfDragonBalls;
        scoreText.text = "Score: " + dragonBall;
    }
}
