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
        Debug.Log(characterStats.currentHealth);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        characterStats.currentHealth -= damage;
        Debug.Log(characterStats.currentHealth);
        if (characterStats.currentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void OnAttackActionEvent()
    {
        //this will be called by our animation, and will calculate how we do damage
    }

    public virtual void Die()
    {
        animator.SetTrigger("Die");
        isAlive = false;
        Debug.Log("Enemy Died");
    }
}
