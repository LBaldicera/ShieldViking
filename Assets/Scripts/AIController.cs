using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : BaseUnit
{
    private enum State
    {
        Idle,
        Patrolling,
        Chasing
    }

    [SerializeField]
    private float lookDistance = 10f;//our AI can see 10 units away
    private State currentState; //this keeps track of the current state

    [SerializeField]
    private float attackDis; //Distance to attack

    [SerializeField]
    private Transform pointA;
    [SerializeField]
    private Transform pointB;

    private Transform currentPoint;
    private Transform lastPoint;

    [SerializeField]
    private Transform eyes;

    [SerializeField]
    private float moveSpeed;

    private Transform target;
    private Vector2 moveDirection;

    private BaseUnit currentEnemy;

    [SerializeField]
    private float viewAngle;

    private float vx;

    private bool facingLeft;

    public override void Start()
    {
        base.Start();
        lastPoint = pointA;

        if (this.gameObject != null)
        {
            SetState(State.Idle);
        }

        //Flip();

    }

    private void Flip()
    {
        // get the current scale
        Vector2 localScale = transform.localScale;
        vx = rb.velocity.x;
        if (vx < 0) // moving right so face right
        {
            facingLeft = true;
        }
        else if (vx > 0)
        { // moving left so face left
            facingLeft = false;
        }

        // check to see if scale x is right for the player
        // if not, multiple by -1 which is an easy way to flip a sprite
        if (((facingLeft) && (localScale.x < 0)) || ((!facingLeft) && (localScale.x > 0)))
        {
            localScale.x *= -1;
        }

        // update the scale
        transform.localScale = localScale;
    }

    private void SetState(State newState)
    {
        //what we want to do here is look at the newstater, compare it to the enumvalues, and then figure out what to do based on that.
        //set state will only be called when a state changes
        currentState = newState;
        StopAllCoroutines();//stop the previous coroutines so they aren't operating at the same time

        switch (currentState)
        {
            case State.Idle:
                StartCoroutine(OnIdle());
                //do some work
                break;
            case State.Patrolling:
                StartCoroutine(OnPatrolling());
               //do some work
                break;
            case State.Chasing:
                StartCoroutine(OnChasing());
                //do some work
                break;
            default:
                break;
        }
        ///
    }

    private IEnumerator OnIdle() //handles our idle state
    {
        animator.SetBool("Walking", false);

        while ((currentPoint == null))
        {
            if (lastPoint == pointA)
            {
                currentPoint = pointB;
            }
            else if (lastPoint == pointB)
            {
                currentPoint = pointA;
            }
            LookForEnemies();

            yield return new WaitForSeconds(2f);
        }


        SetState(State.Patrolling); //we found a point, we now need to move

    }

    private IEnumerator OnPatrolling()
    {
        animator.SetBool("Walking", true);

        while (currentPoint != null && isAlive)
        {
            Vector3 direction = (currentPoint.position - transform.position).normalized;
            rb.velocity = new Vector2(direction.x, 0f) * moveSpeed;

            LookForEnemies();
            Flip();

            if (Vector3.Distance(transform.position, currentPoint.position) <= 0.2f)
            {
                lastPoint = currentPoint;
                currentPoint = null;
            }
           
            yield return null;
        }

        //After Value turns 1, he is going to search for a new spot
        SetState(State.Idle);

    }

    private IEnumerator OnChasing()
    {
        
        float attackTimer = characterStats.attackRate;

        while (currentEnemy != null && currentEnemy.isAlive && isAlive)
        {
            attackTimer += Time.deltaTime; //increment our shoot timer each time
            float distanceToEnemy = Vector2.Distance(currentEnemy.transform.position, this.transform.position);
            //if we are too far away or we can't see our enemy, let's move towards them
            //otherwise, if our shoot timer is up, shoot them

            Vector2 dir = currentEnemy.transform.position - this.transform.position;
            dir.Normalize();

            Transform enemyPos = currentEnemy.transform;
            //transform.LookAt(enemyPos.position);


            if (distanceToEnemy > attackDis )
            {
                animator.SetBool("Walking", true);
                rb.velocity = new Vector2(dir.x, 0f) * moveSpeed;
                Flip();

            }
            else if (attackTimer > characterStats.attackRate)
            {
                attackTimer = 0;

                //if this is true, we attack the player
                animator.SetBool("Walking", false);
                animator.SetTrigger("Attack");

                yield return new WaitForSecondsRealtime(characterStats.attackRate); //Wait for the animation to complete before actaully reducing life

            }
            yield return null;
        }
        currentEnemy = null;
        currentPoint = pointA;
        SetState(State.Idle);
    }

    private void LookForEnemies()
        {
            Collider2D[] surroundingColliders = Physics2D.OverlapCircleAll(this.transform.position, lookDistance);
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
                        SetState(State.Chasing);
                        return; //remember: you can return anywhere in a void function and it immediately exits
                    }
                }
            }
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

    protected Vector2 GetEyesPosition()
    {
        return (eyes.position);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointA.transform.position, 0.1f);
        Gizmos.DrawWireSphere(pointB.transform.position, 0.1f);
        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);

        // Draw a box to represent the field of vision
        Gizmos.color = Color.red; // You can choose any color you like
        Gizmos.DrawWireCube(GetEyesPosition(), new Vector3(lookDistance * 2, lookDistance * 2, 0));
    }

    public override void Die()
    {
        base.Die();
        StopAllCoroutines();
        rb.constraints = RigidbodyConstraints2D.FreezePosition;
        GetComponent<Collider2D>().enabled = false;

    }


}
