using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    public Sprite Flag;
    public Sprite FlagGreen;
    private SpriteRenderer checkpointSpriteRenderer;
    public bool checkpointReached;

    // Start is called before the first frame update
    void Start()  {
        checkpointSpriteRenderer = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)    {
        if(other.tag == "Player")   {
            //When player enters box collider of checkpoint
            //Flag changes from original sprite to checkpointReached sprite 
            checkpointSpriteRenderer.sprite = FlagGreen;
            checkpointReached = true;
        }
    }
}
