using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUnit : MonoBehaviour
{
    public CharacterStats characterStats;
   

    // Start is called before the first frame update
    void Start()
    {
        characterStats.currentHealth = characterStats.startingHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        characterStats.currentHealth -= damage;
        if (characterStats.currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy Died");
    }
}
