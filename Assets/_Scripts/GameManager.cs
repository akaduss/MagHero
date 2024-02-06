using System.Collections.Generic;
using UnityEngine;
using Akadus.HealthSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool isPlayed;

    private List<GameObject> enemies;
    //[SerializeField] private GameObject portal;
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

    // Start is called before the first frame update
    void Start()
    {
        enemies = GetComponent<Breeze.Addons.Spawner.BreezeSpawnerCore>().systemsSpawned;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsAllEnemiesDead())
        {
            if(isPlayed == false)
            {
                isPlayed = true;
                GetComponent<MoreMountains.Feedbacks.MMF_Player>().CanPlayWhileAlreadyPlaying = false;
                GetComponent<MoreMountains.Feedbacks.MMF_Player>().PlayFeedbacks();
                //portal.SetActive(true);
                PullGoldToPlayer();
            }

        }
    }

    bool IsAllEnemiesDead()
    {
        int count = enemies.Count;
        int dead = 0;

        foreach (var enemy in enemies)
        {
            if (enemy.GetComponent<Health>().CurrentHealth <= 0f)
            {
                dead++;
            }

        }

        if (count == dead)
        {
            return true;
        }

        return false;
    }

    void PullGoldToPlayer()
    {
        // Find all gold objects in the scene
        Pickup[] goldObjects = FindObjectsOfType<Pickup>(); // Adjust the tag accordingly
        Transform playerTransform = FindObjectOfType<PlayerController>().transform;

        foreach (var goldObject in goldObjects)
        {

            // Calculate the direction from the gold object to the player
            Vector3 directionToPlayer = playerTransform.position - goldObject.transform.position;

            // Move the gold object towards the player
            goldObject.transform.Translate(pullSpeed * Time.deltaTime * directionToPlayer.normalized);
        }
    }
}
