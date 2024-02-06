using UnityEngine;
using Akadus.HealthSystem;
using System;

[Serializable]
public class PlayerStats
{
    public int gold;

    [Space(5)]
    [Header("Health")]
    public Health health;

    [Header("Movement")]
    public float moveSpeed = 5f;
    public float gravity = -9.81f;

    [Header("Combat")]
    public float attackRange = 2f;
    public float attackRate = 1.0f;
    [SerializeField] private float attackDamage = 20f;
    public static bool isCrit;

    public float AttackDamage
    {
        get
        {
            // Check for a critical hit based on the criticalChance
            if (UnityEngine.Random.value < criticalChance)
            {
                Debug.Log("big damage");
                isCrit = true;
                return CriticalDamage;
            }
            else
            {
                isCrit = false;
                return attackDamage;
            }
        }
    }

    public float criticalChance = 0.4f;
    public float criticalMultiplier = 2f;

    public float CriticalDamage
    {
        get { return attackDamage * criticalMultiplier; }
    }

}