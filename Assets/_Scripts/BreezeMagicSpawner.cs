using Breeze.Core;
using UnityEngine;

public class BreezeMagicSpawner : MonoBehaviour
{
    [Header("Settings")]
    public BreezeSystem System;
    public GameObject magicEffectPrefab;
    public Transform throwPosition;
    public float throwForce = 15f;

    public void InitiateThrow()
    {
        GameObject spawnedTarget = Instantiate(magicEffectPrefab, throwPosition.position, Quaternion.identity);
        var targetRigidbody = spawnedTarget.GetComponent<Rigidbody>();
        var targetScript = spawnedTarget.GetComponent<BreezeMagicHit>();

        targetScript.System = System;

        // Calculate the direction to the target
        Vector3 directionToTarget =  System.GetTarget().transform.position - targetRigidbody.transform.position;
        print(System.GetTarget().transform.position);
        directionToTarget.y = 0;
        Vector3 force = directionToTarget.normalized * throwForce;


        spawnedTarget.transform.forward = directionToTarget.normalized;
        // Apply the force to the target object's Rigidbody
        targetRigidbody.AddForce(force);
        print(targetRigidbody + " rb");
    }
}