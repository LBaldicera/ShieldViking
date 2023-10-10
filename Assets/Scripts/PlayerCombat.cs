using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private Transform attackPoint;
    [SerializeField] float attackRange = 0.5f;
    [SerializeField] LayerMask enemyLayers;

    public Animator animator;
    public CharacterStats characterStats;

    private void Start()
    {
        characterStats.nextAttackTime = 0;
    }
    void Update()
    {
        if (Time.time >= characterStats.nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                Debug.Log("Im attacking");
                Attack();
                characterStats.nextAttackTime = Time.time + 1f / characterStats.attackRate;
            }
        }
    }

    void Attack()
    {
        animator.SetTrigger("Attack");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemies>().TakeDamage(characterStats.attackDamage);
        }
    }
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
