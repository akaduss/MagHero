using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    [SerializeField] private float _missileDamage;


    private void OnCollisionEnter(Collision collision)
    {
        print(collision.transform.name);
        if (collision.transform.TryGetComponent(out IDamageable component))
        {
            component.TakeDamage(_missileDamage);
            Destroy(this);
        }
        else
        {
            Destroy(this);
        }

    }
}
