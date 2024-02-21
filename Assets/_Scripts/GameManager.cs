using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static event Action OnAllEnemiesDefeated;

    public bool isPlayed;
    [SerializeField] private float pullSpeed = 3f;


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

    private void OnEnable()
    {
        EnemyManager.OnAllEnemiesDefeated += UnlockNextLevel;
    }

    private void OnDisable()
    {
        EnemyManager.OnAllEnemiesDefeated -= UnlockNextLevel;
    }

    private void UnlockNextLevel()
    {
        Debug.Log("All enemies defeated! Unlocking next level...");
        OnAllEnemiesDefeated?.Invoke();
        isPlayed = true;
        GetComponent<MoreMountains.Feedbacks.MMF_Player>().PlayFeedbacks();

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
