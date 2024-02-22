using Akadus.HealthSystem;
using Breeze.Core;
using UnityEngine;

public class BreezeMagicHit : MonoBehaviour
{
    [Header("Settings")]
    [HideInInspector] public BreezeSystem System;
    public float damageAmount;
    public GameObject flash;

    void Start()
    {
        if (flash != null)
        {
            var flashInstance = Instantiate(flash, transform.position, Quaternion.identity);
            flashInstance.transform.forward = gameObject.transform.forward;
            var flashPs = flashInstance.GetComponent<ParticleSystem>();
            if (flashPs != null)
            {
                Destroy(flashInstance, flashPs.main.duration);
            }
            else
            {
                var flashPsParts = flashInstance.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(flashInstance, flashPsParts.main.duration);
            }
        }
        Destroy(gameObject, 5);
    }

    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<Health>().TakeDamage(damageAmount);
        //Destroy(gameObject);

    }


}
