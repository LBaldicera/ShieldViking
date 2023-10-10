using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    public CharacterStats characterStats;
   

    // Start is called before the first frame update
    void Start()
    {
        characterStats.currentHealth = characterStats.maximumHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        characterStats.currentHealth -= characterStats.attackDamage;
        if (characterStats.currentHealth < 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy Died");
    }
}
