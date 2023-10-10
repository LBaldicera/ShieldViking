using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] public float startingHealth;
    public float currentHealth {get; private set;}
    // Start is called before the first frame update
    private void Awake()
    {
        currentHealth = startingHealth;
    }

    private void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
        
        if (currentHealth > 0)
        {
            //hurt
        }
        else
        {
            //dead
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            currentHealth -= 0.1f;
        }
        Debug.Log("Player's current health: " + currentHealth);
    }

    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }
}
