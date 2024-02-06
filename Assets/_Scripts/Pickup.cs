using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] private AudioClip[] singleSFXs;
    [SerializeField] private AudioClip[] pourSFXs;
    [SerializeField] private int goldValue;
    [SerializeField] private float sfxVolume;

    private void OnTriggerEnter(Collider other)
    {
        AudioClip singleSFX = singleSFXs[Random.Range(0, singleSFXs.Length)];
        AudioClip pourSFX = pourSFXs[Random.Range(0, pourSFXs.Length)];

        if (other.gameObject.GetComponent<PlayerController>() != null)
        {
            int rng = Akadus.Randomizer.GenerateGem();

            if(rng == 1)
            {
                AudioSource.PlayClipAtPoint(singleSFX, Camera.main.transform.position, sfxVolume);
                goldValue = Random.Range(1,3);
            }
            else if (rng == 2)
            {
                AudioSource.PlayClipAtPoint(pourSFX, Camera.main.transform.position, sfxVolume);
                goldValue = Random.Range(3, 10);
            }


            other.gameObject.GetComponent<PlayerController>().PlayerStats.gold += goldValue;
            GetComponent<Collider>().enabled = false;
            transform.GetChild(0).gameObject.SetActive(false);
            Destroy(this, 1f);
        }
    }
}
