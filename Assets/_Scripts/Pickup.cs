using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] private AudioClip pickupSound;
    [SerializeField] private int goldValue;

    private void OnTriggerEnter(Collider other)
    {
        AudioSource.PlayClipAtPoint(pickupSound, transform.position);
        if (other.gameObject.GetComponent<PlayerController>() != null)
        {
            goldValue *= Random.Range(1,4);
            other.gameObject.GetComponent<PlayerController>().PlayerStats.gold += goldValue;
            GetComponent<Collider>().enabled = false;
            transform.GetChild(0).gameObject.SetActive(false);
            Destroy(this, 1f);
        }
    }
}
