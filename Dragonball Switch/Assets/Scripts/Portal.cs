using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    private GameObject portalTrigger;
    private Animator portalAnimation;

    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
       
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("LAODING");
            SceneManager.LoadScene("BossScene");
        }

    }

    private void Start()
    {
        portalAnimation = GetComponent<Animator>();
    }
}
