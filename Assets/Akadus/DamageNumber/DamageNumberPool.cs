using System.Collections.Generic;
using UnityEngine;

public class DamageNumberPool : MonoBehaviour
{
    public static DamageNumberPool Instance;

    public GameObject damageNumberPrefab;
    public int poolSize = 10;

    private List<GameObject> damageNumberPool;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        InitializePool();
    }

    private void InitializePool()
    {
        damageNumberPool = new List<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject damageNumber = Instantiate(damageNumberPrefab);
            damageNumber.SetActive(false);
            damageNumberPool.Add(damageNumber);
        }
    }

    public GameObject GetDamageNumber(Vector3 position)
    {
        // Find an inactive damage number from the pool
        foreach (GameObject damageNumber in damageNumberPool)
        {
            if (!damageNumber.activeInHierarchy)
            {
                damageNumber.transform.position = position;
                damageNumber.SetActive(true);
                return damageNumber;
            }
        }

        // If all damage numbers are active, instantiate a new one (expand the pool)
        GameObject newDamageNumber = Instantiate(damageNumberPrefab);
        newDamageNumber.SetActive(false);
        damageNumberPool.Add(newDamageNumber);
        newDamageNumber.transform.position = position;

        return newDamageNumber;
    }
}
