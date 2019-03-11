using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleScript : MonoBehaviour
{
    public int dragonBallScore = 0;
    // Start is called before the first frame update
    void Start()  {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    private void OnTriggerEnter2D(Collider2D other) { 
        Destroy(gameObject);
        dragonBallScore += 1;
    }
}
