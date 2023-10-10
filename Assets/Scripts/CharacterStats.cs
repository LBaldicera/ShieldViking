using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character Stats", menuName = "Character/Character Stats")]
public class CharacterStats : ScriptableObject
{
    public float startingHealth;
    public float currentHealth;
    public int attackDamage = 2;
    public float attackRate = 2f;
    public float nextAttackTime = 0f;
}
