using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BaseUnit : MonoBehaviour
{
    public CharacterStats characterStats;
    [SerializeField] protected Animator animator;

    public bool isAlive;
    public bool isBlocking;

    protected AudioSource audioSource;
    [SerializeField]
    protected AudioClip getHitClip;
    [SerializeField]
    protected AudioClip getHitShieldClip;
    [SerializeField]
    protected AudioClip jumpClip;
    [SerializeField]
    protected AudioClip deathClip;
    [SerializeField]
    protected AudioClip slashClip;
    [SerializeField]
    protected AudioClip collectibleClip;


    protected float currentHealth;


    protected Rigidbody2D rb;


    // Start is called before the first frame update
    public virtual void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        isAlive = true;
        currentHealth = characterStats.startingHealth;
        characterStats.currentHealth = characterStats.startingHealth;

        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(float damage, bool shield)
    {
        animator.SetTrigger("GetHit");
        currentHealth -= damage;
        if (shield)
        {
            audioSource.clip = getHitShieldClip;
        }
        else
        {
            audioSource.clip = getHitClip;
        }
        audioSource.Play();
        Debug.Log(currentHealth);
        characterStats.currentHealth = currentHealth;
        if (currentHealth <= 0)
        {
            audioSource.clip = deathClip;
            audioSource.Play();
            Die();
        }
    }

    public virtual void OnAttackActionEvent()
    {
        //this will be called by our animation, and will calculate how we do damage
    }

    public virtual void Die()
    {
        isAlive = false;
        animator.SetBool("IsAlive", false);
        animator.SetTrigger("Death");
    }

    public void GameOver()
    {
        SceneManager.LoadScene(3); 
    }
}
