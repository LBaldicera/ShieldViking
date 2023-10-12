using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private CharacterStats characterStats;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.gameObject.layer == LayerMask.NameToLayer("Enemy")) || (other.gameObject.layer == LayerMask.NameToLayer("Player")))
        {
            other.GetComponent<BaseUnit>().TakeDamage(characterStats.attackDamage);
        }

        if ((other.gameObject.layer == LayerMask.NameToLayer("Shield")))
        {
            other.GetComponentInParent<BaseUnit>().TakeDamage(characterStats.attackDamage / 4f);
        }
    }
}
