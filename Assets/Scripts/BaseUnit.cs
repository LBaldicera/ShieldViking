using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUnit : MonoBehaviour
{
    public CharacterStats characterStats;
    [SerializeField] protected Animator animator;

    public bool isAlive;

    protected Rigidbody2D rb;


    // Start is called before the first frame update
    public virtual void Start()
    {
        isAlive = true;
        characterStats.currentHealth = characterStats.startingHealth;
        rb = GetComponent<Rigidbody2D>();
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
