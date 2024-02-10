using Breeze.Core;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Missile : MonoBehaviour
{
    [HideInInspector] public float MissileDamage;
    public GameObject explosion;
    public AudioClip collisionSound;
    public LayerMask layer;

    private void OnCollisionEnter(Collision collision)
    {

        // Play collision sound when the missile collides with something
        if (collisionSound != null)
        {
            AudioSource.PlayClipAtPoint(collisionSound, transform.position);
        }

        if (collision.transform.TryGetComponent(out IDamageable component))
        {

            if (collision.transform.GetComponent<BreezeDamageBase>() != null)
            {
                collision.transform.GetComponent<BreezeDamageable>().TakeDamage(MissileDamage, gameObject, true);
            }
            component.TakeDamage(MissileDamage);
        }
        else if (collision.gameObject.layer == layer)
        {
            Instantiate(explosion);
        }
        Destroy(gameObject);

    }
}
