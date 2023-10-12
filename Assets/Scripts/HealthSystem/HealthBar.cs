using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    [SerializeField] private HealthSystem playerHealth;
    [SerializeField] private Image totalHealthbar;
    [SerializeField] private Image currentHealthbar;
    [SerializeField] private float maxHealthBar;
    public CharacterStats characterStats;


    private void Start()
    { 

    }

    private void Update()
    {
        currentHealthbar.fillAmount = maxHealthBar * (characterStats.currentHealth / characterStats.startingHealth);

    }
}
