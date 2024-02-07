using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static event Action OnAllEnemiesDefeated;

    public bool isPlayed;

    [SerializeField] private float pullSpeed = 3f;

    public List<EnemyDeath> enemies = new();


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    void Update()
    {
        if (isPlayed)
        {
            PullGoldToPlayer();
        }
    }

    public void Subscrige(EnemyDeath enemyDeath)
    {
        enemies.Add(enemyDeath);
        enemyDeath.OnEnemyDie += EnemyDefeated;
    }

    private void EnemyDefeated()
    {
        // Check if all enemies are defeated
        if (AreAllEnemiesDefeated())
        {
            // Trigger event when all enemies are defeated
            OnAllEnemiesDefeated?.Invoke();
            enemies = null;

            isPlayed = true;
            GetComponent<MoreMountains.Feedbacks.MMF_Player>().CanPlayWhileAlreadyPlaying = false;
            GetComponent<MoreMountains.Feedbacks.MMF_Player>().PlayFeedbacks();
            //portal.SetActive(true);
            PullGoldToPlayer();

        }
    }

    private bool AreAllEnemiesDefeated()
    {
        foreach (var enemy in enemies)
        {
            if (enemy.GetComponent<Akadus.HealthSystem.Health>().CurrentHealth > 0f)
            {
                // If at least one enemy is still active, return false
                return false;
            }
        }
        return true;
    }

    void PullGoldToPlayer()
    {
        // Find all gold objects in the scene
        Pickup[] goldObjects = FindObjectsOfType<Pickup>(); // Adjust the tag accordingly
        Transform playerTransform = PlayerController.Instance.transform;

        foreach (var goldObject in goldObjects)
        {

            // Calculate the direction from the gold object to the player
            Vector3 directionToPlayer = playerTransform.position - goldObject.transform.position;

            // Move the gold object towards the player
            goldObject.transform.Translate(pullSpeed * Time.deltaTime * directionToPlayer.normalized);
        }
    }
}


