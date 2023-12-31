using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AIArcher : BaseUnit
{
    [SerializeField]
    Transform arrowStartPos;

    [SerializeField]
    GameObject arrowPrefab;

    [SerializeField]
    private float attackDis; //Distance to attack

    [SerializeField]
    private Transform eyes;
    private BaseUnit currentEnemy;

    [SerializeField]
    private float viewAngle;

    [SerializeField]
    protected AudioClip attackClip;

    private float attackTimer;
    private bool hasFlipped = false;

    public GameObject player;


    public override void Start()
    {
        base.Start();
        attackTimer = characterStats.attackRate;
        player = GameObject.Find("Player");
    }

    private void FixedUpdate()
    {
        attackTimer += Time.fixedDeltaTime;
        if(characterStats.attackRate < attackTimer && currentEnemy != null && isAlive)
        {
            animator.SetTrigger("Attack");
            attackTimer = 0f;
            Debug.Log(currentEnemy);
        }
        if (isAlive)
        {
            LookForEnemies();
            FlipCondition();
        }

    }

    // Start is called before the first frame update
    public override void OnAttackActionEvent()
    {
        base.OnAttackActionEvent();
        if (currentEnemy != null)
        {
            GameObject Arrow = Instantiate(arrowPrefab, arrowStartPos.position, arrowStartPos.rotation);
            Arrow.GetComponent<Arrow>().Init(currentEnemy, characterStats.attackDamage);
            audioSource.clip = attackClip;
            audioSource.Play();
        }
    }

    private void LookForEnemies()
    {
        Collider2D[] surroundingColliders = Physics2D.OverlapCircleAll(this.transform.position, attackDis);
        foreach (Collider2D coll in surroundingColliders)
        {
            //how do we know if the element we are colliding with is an enemy?
            //Not on our team.
            BaseUnit unit = coll.GetComponent<BaseUnit>();
            if (unit != null && isAlive)
            {
                //we also want to check a couplemore things:
                if (unit.tag == "Player" && CanSee(unit.transform, unit.transform.position) && unit.isAlive)
                {
                    currentEnemy = unit;
                    return; //remember: you can return anywhere in a void function and it immediately exits
                }
                else
                {
                    currentEnemy = null;
                }
            }
        }
    }

    protected Vector2 GetEyesPosition()
    {
        return (eyes.position);
    }


    private bool CanSee(Transform target, Vector2 targetPosition)
    {
        Vector2 startPos = GetEyesPosition(); //where we do get the starting position of our vision?
        Vector2 dir = targetPosition - startPos;
        //We now need to change if our angle is greater than the viewing angle, and, if so, return false
        if (Vector2.Angle(transform.forward, dir) > viewAngle)
            return false;

        return true;
    }

    public override void Die()
    {
        base.Die();
        StopAllCoroutines();
        rb.constraints = RigidbodyConstraints2D.FreezePosition;
        GetComponent<Collider2D>().enabled = false;

    }

    private void FlipCondition()
    {
        if (player.transform.position.x > transform.position.x && !hasFlipped)
        {
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
            Debug.Log("I'm flipping");

            // Set the hasFlipped flag to true to prevent further flips.
            hasFlipped = true;
        }
        if (player.transform.position.x < transform.position.x && hasFlipped)
        {
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
            Debug.Log("I'm flipping");

            // Set the hasFlipped flag to true to prevent further flips.
            hasFlipped = false;
        }
    }
}
