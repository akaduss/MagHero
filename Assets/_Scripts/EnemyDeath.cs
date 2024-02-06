using UnityEngine;

public class EnemyDeath : MonoBehaviour, IDeathHandler
{
    public Transform goldDropPrefab;
    public float spreadRadius = 1.0f; // Radius within which drops will spread

    public void HandleDeath()
    {
        int randomCount = Random.Range(1, 3);

        for (int i = 0; i < randomCount; i++)
        {
            // Calculate random angle within a circle
            float angle = Random.Range(0f, 360f);

            // Convert polar coordinates to Cartesian coordinates
            float x = Mathf.Cos(angle * Mathf.Deg2Rad) * spreadRadius;
            //float y = Mathf.Sin(angle * Mathf.Deg2Rad) * spreadRadius;

            // Apply offsets to spawn position
            Vector3 spawnPosition = new(transform.position.x + x, transform.position.y, transform.position.z);

            // Instantiate gold drops with random positions
            Instantiate(goldDropPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
