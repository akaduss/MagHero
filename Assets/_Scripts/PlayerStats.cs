using UnityEngine;
using Akadus.HealthSystem;

[System.Serializable]
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

    public float AttackDamage
    {
        get
        {
            // Check for a critical hit based on the criticalChance
            if (Random.value < criticalChance)
            {
                // Apply critical damage
                Debug.Log("big damage");
                return CriticalDamage;
            }
            else
            {
                // Return regular damage
                return attackDamage;
            }
        }
    }

    public float criticalChance = 0.4f;
    public float criticalMultiplier = 2f;

    // Read-only property for criticalDamage
    public float CriticalDamage
    {
        get { return attackDamage * criticalMultiplier; }
    }

    // Add other stats as needed

    public PlayerStats()
    {
        // Initialize default values
    }


}
