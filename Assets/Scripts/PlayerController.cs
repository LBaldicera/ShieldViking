using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : BaseUnit
{
    [SerializeField]
    private float moveSpeed = 5.0f;
    private float defaultMoveSpeed;
    [SerializeField]
    private float jumpForce = 5.0f;
    private bool isGrounded;
    private bool isAttacking;


    private float horizontal;
    private bool isFacingRight;

    [SerializeField] private Transform attackPoint;
    [SerializeField] float attackRange = 0.5f;
    [SerializeField] LayerMask enemyLayers;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;


    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        characterStats.nextAttackTime = 0;
        defaultMoveSpeed = moveSpeed;
        isAlive = true;

    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Speed", Mathf.Abs(horizontal));
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded() && isAlive)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            animator.SetTrigger("IsJumping");
            audioSource.clip = jumpClip;
            audioSource.Play();
        }

        if (Input.GetKey(KeyCode.UpArrow) && IsGrounded())
        {
            animator.SetBool("IsBlocking", true);
            isBlocking = true;
        }
        else
        {
            if (isAlive)
            {
                animator.SetBool("IsBlocking", false);
                isBlocking = false;
            }
        }

        if (isBlocking || isAttacking || !isAlive)
        {
            moveSpeed = 0f;
        }
        else
        {
            moveSpeed = defaultMoveSpeed;
        }

        if (transform.position.y < -1.5f)
        {
            Die();

        }

        Flip();
        animator.SetBool("IsGrounded", IsGrounded());

        if (Time.time >= characterStats.nextAttackTime)
        {
            isAttacking = false;
            if (Input.GetKeyDown(KeyCode.X) && IsGrounded())
            {
                audioSource.clip = slashClip;
                audioSource.Play();
                isAttacking = true;
                animator.SetTrigger("Attack");
                characterStats.nextAttackTime = Time.time + 1f / characterStats.attackRate;
            }
        }
    }


    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);

        if (transform.position.y < -1.5f)
        {
            SceneManager.LoadSceneAsync(2);
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal > 0f || !isFacingRight && horizontal < 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public void AddHealth(float health)
    {
        currentHealth += health;
        characterStats.currentHealth = currentHealth;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the colliding object is on the "Collectibles" layer
        if (collision.gameObject.layer == LayerMask.NameToLayer("Collectibles"))
        {
            audioSource.clip = collectibleClip;
            audioSource.Play();
        }
    }

}
