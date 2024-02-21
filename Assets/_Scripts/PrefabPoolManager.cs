using System.Collections.Generic;
using UnityEngine;

public class PrefabPoolManager : MonoBehaviour
{
    public GameObject[] propPrefabs;
    public GameObject[] obstaclePrefabs;
    public GameObject[] enemyPrefabs;

    private List<GameObject> propPool = new List<GameObject>();
    private List<GameObject> obstaclePool = new List<GameObject>();
    private List<GameObject> enemyPool = new List<GameObject>();

    public GameObject GetRandomProp()
    {
        if (propPool.Count == 0)
            InitializePool(propPrefabs, propPool);

        return GetObjectFromPool(propPool);
    }

    public GameObject GetRandomObstacle()
    {
        if (obstaclePool.Count == 0)
            InitializePool(obstaclePrefabs, obstaclePool);

        return GetObjectFromPool(obstaclePool);
    }

    private void InitializePool(GameObject[] prefabs, List<GameObject> pool)
    {
        foreach (GameObject prefab in prefabs)
        {
            GameObject obj = Instantiate(prefab, transform);
            obj.SetActive(false);
            pool.Add(obj);
        }
    }

    private GameObject GetObjectFromPool(List<GameObject> pool)
    {
        GameObject obj = pool[Random.Range(0, pool.Count)];
        pool.Remove(obj);
        obj.SetActive(true);
        return obj;
    }

    public void ReturnObjectToPool(GameObject obj)
    {
        obj.SetActive(false);
        if (obj.CompareTag("Prop"))
            propPool.Add(obj);
        else if (obj.CompareTag("Obstacle"))
            obstaclePool.Add(obj);
    }
}
