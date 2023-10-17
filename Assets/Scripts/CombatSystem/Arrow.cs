using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; //this is our tweening library

public class Arrow : MonoBehaviour
{
    [SerializeField]
    float speed = 0.5f;

    private float attackPower;
    private BaseUnit attackTarget;


    public void Init(BaseUnit target, float power)
    {
        attackPower = power;
        attackTarget = target;
        //find the location we are attacking
        Vector3 targetPos = target.GetComponent<Collider2D>().bounds.center;
        Quaternion rotation = Quaternion.LookRotation(targetPos - transform.position, transform.TransformDirection(Vector2.up));
        transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);

        Tweener moveTween = transform.DOMove(targetPos, speed); //notice that we don't provide a duration and we only provide a speed; the reason for this is that we don't want a fixed duration in case objects are farther or closer
        moveTween?.SetSpeedBased(true);//make sure that we are setting the tween to be speed based
        moveTween?.OnComplete(OnProjectileArrived);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.gameObject.layer == LayerMask.NameToLayer("Shield")))
        {
            other.GetComponentInParent<BaseUnit>().TakeDamage(attackPower / 8f, true);
            OnProjectileArrived();

        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            other.GetComponent<BaseUnit>().TakeDamage(attackPower, false);
            OnProjectileArrived();

        }

    }

    private void OnProjectileArrived() //we want this function to be called when we actually make it to our target
    {
        Destroy(this.gameObject);
    }
}
