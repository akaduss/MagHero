using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyDeath : MonoBehaviour, IDeathHandler
{
    public static event Action<float> OnEnemyDeath;


    public float increaseXP = 20f;
    public Transform goldDropPrefab;
    public ParticleSystem particle;
    public ParticleSystem bigParticle;

    public float spreadRadius = 1.0f; // Radius within which drops will spread

    public void HandleDeath()
    {
        OnEnemyDeath?.Invoke(increaseXP);

        int rng = Akadus.Randomizer.GenerateGem();
        int randomCount;
        if (rng == 1)
        {
            particle.Play();
            randomCount = Random.Range(1, 3);
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
        if (rng == 2)
        {
            bigParticle.Play();
            randomCount = Random.Range(3, 6);
            for (int i = 0; i < randomCount; i++)
            {
                float angle = Random.Range(0f, 360f);

                float x = Mathf.Cos(angle * Mathf.Deg2Rad) * spreadRadius;

                Vector3 spawnPosition = new(transform.position.x + x, transform.position.y, transform.position.z);

                Instantiate(goldDropPrefab, spawnPosition, Quaternion.identity);
            }

        }
    }
}
