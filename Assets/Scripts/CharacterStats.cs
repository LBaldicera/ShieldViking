using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character Stats", menuName = "Character/Character Stats")]
public class CharacterStats : ScriptableObject
{
    public int maximumHealth = 100;
    public int currentHealth = 0;
    public int attackDamage = 10;
    public float attackRate = 2f;
    public float nextAttackTime = 0f;
}
