using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public CharacterStats characterStats;
    public BaseUnit baseUnit;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            characterStats.currentHealth -= 10f;
            Debug.Log(characterStats.currentHealth);
        }
    }

    public void AddHealth(float _value)
    {
        characterStats.currentHealth = Mathf.Clamp(characterStats.currentHealth + _value, 0, characterStats.startingHealth);
    }
}
