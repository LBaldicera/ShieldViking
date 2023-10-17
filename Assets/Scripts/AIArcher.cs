using System.Collections;
using System.Collections.Generic;
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



    public override void Start()
    {
        base.Start();
    }

    private void FixedUpdate()
    {
        characterStats.nextAttackTime += Time.fixedDeltaTime;
        if(characterStats.attackRate < characterStats.nextAttackTime && currentEnemy != null)
        {
            animator.SetTrigger("Attack");
            characterStats.nextAttackTime = 0f;
        }

        LookForEnemies();

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


}
