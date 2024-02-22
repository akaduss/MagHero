using UnityEngine;
using System;
using System.Collections.Generic;
public class EnemyManager : MonoBehaviour
{
    public static event Action OnAllEnemiesDefeated;

    private List<EnemyDeath> enemies = new();
    private int remainingEnemies;

    private void OnEnable()
    {
        Portal.OnLoadNextScene += Portal_OnLoadNextScene;
        EnemyDeath.OnEnemyDie += HandleEnemyDeath;

    }

    private void OnDisable()
    {
        Portal.OnLoadNextScene -= Portal_OnLoadNextScene;
        EnemyDeath.OnEnemyDie -= HandleEnemyDeath;
    }

    private void Start()
    {
        // Find all enemies in the scene and add them to the list
        EnemyDeath[] enemyArray = FindObjectsOfType<EnemyDeath>();
        enemies.AddRange(enemyArray);
        remainingEnemies = enemies.Count;
        if(remainingEnemies <= 0)
        {
            OnAllEnemiesDefeated?.Invoke();
        }
    }

    private void Portal_OnLoadNextScene(Vector3 obj)
    {
        enemies = new();
        EnemyDeath[] enemyArray = FindObjectsOfType<EnemyDeath>();
        enemies.AddRange(enemyArray);
        remainingEnemies = enemies.Count;
    }

    private void HandleEnemyDeath()
    {
        remainingEnemies--;
        // Check if all enemies are defeated
        if (remainingEnemies <= 0)
        {
            OnAllEnemiesDefeated?.Invoke();
        }
    }
}
