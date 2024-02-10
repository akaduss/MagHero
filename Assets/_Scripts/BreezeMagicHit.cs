using Breeze.Core;
using UnityEngine;

public class BreezeMagicHit : MonoBehaviour
{
    [Header("Settings")]
    [HideInInspector] public BreezeSystem System;
    public float damageAmount;

    private void OnTriggerEnter(Collider other)
    {
        //Destroy(gameObject);

        if (other.GetComponent<BreezeDamageable>() != null)
        {
            other.GetComponent<BreezeDamageable>().TakeDamage(damageAmount, System.gameObject, false);
        }
    }
}
