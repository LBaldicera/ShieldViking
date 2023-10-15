using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BaseUnit : MonoBehaviour
{
    public CharacterStats characterStats;
    [SerializeField] protected Animator animator;

    public bool isAlive;
    public bool isBlocking;


    protected Rigidbody2D rb;


    // Start is called before the first frame update
    public virtual void Start()
    {
        animator = GetComponent<Animator>();
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
        if (characterStats.currentHealth <= 0 && isAlive)
        {
            animator.SetTrigger("Death");
            GetComponent<Collider2D>().enabled = false;
            isAlive = false;
            rb.constraints = RigidbodyConstraints2D.FreezePositionY;
        }
        if (isBlocking)
        {
            animator.SetTrigger("Death");
            GetComponent<Collider2D>().enabled = false;
            isAlive = false;
            rb.constraints = RigidbodyConstraints2D.FreezePositionY;

        }


    }

    public void GameOver()
    {
        SceneManager.LoadScene(2); // Load the next scene (change the index as needed)
    }
}
