using UnityEngine;

public class SkillSystem : MonoBehaviour
{
    public GameObject projectilePrefab;
    public AudioClip spawnSound;
    public AudioClip collisionSound;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void DefaultAttack(Transform target, float damage)
    {
        // projectile.GetComponent<Projectile>().ConfigureProjectile(targetPosition);

        GameObject go = Instantiate(projectilePrefab, transform.position + Vector3.up, Quaternion.LookRotation(target.position - transform.position));
        go.GetComponent<Missile>().MissileDamage = damage;
        go.GetComponent<Rigidbody>().velocity = (target.position - transform.position).normalized * 10f;


        if (spawnSound != null)
        {
            audioSource.PlayOneShot(spawnSound);
        }
    }
}
