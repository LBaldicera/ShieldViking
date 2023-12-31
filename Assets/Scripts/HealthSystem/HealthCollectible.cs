using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    [SerializeField] private float healthValue;

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Define a layer mask for the "Player" layer
        int playerLayer = LayerMask.NameToLayer("Player");

        // Check if the collided object is on the "Player" layer
        if (collision.gameObject.layer == playerLayer)
        {
            collision.GetComponent<PlayerController>().AddHealth(healthValue);
            gameObject.SetActive(false);
        }
    }

}
