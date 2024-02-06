using System.Collections.Generic;
using UnityEngine;
using Akadus.HealthSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private List<GameObject> enemies;
    [SerializeField] private GameObject portal;

    private void Awake()
    {
        if(Instance == null)
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
            print("All dead");
            portal.SetActive(true);

        }
    }

    bool IsAllEnemiesDead()
    {
        int count = enemies.Count;
        int dead = 0;

        foreach(var enemy in enemies)
        {
            if(enemy.GetComponent<Health>().CurrentHealth <= 0f)
            {
                dead++;
            }

        }

        if(count == dead)
        {
            return true;
        }

        return false;
    }
}
