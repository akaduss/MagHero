using UnityEngine;
using TMPro;

public class DamageNumber : MonoBehaviour
{
    public TextMeshPro damageText;
    public float displayDuration = 1.5f;
    public float moveSpeed = 1f;

    private void Awake()
    {
        damageText = GetComponent<TextMeshPro>();
    }

    private void OnEnable()
    {
        // Reset the damage number when it's reused from the pool
        damageText.text = string.Empty;
        Invoke(nameof(DisableDamageNumber), displayDuration);
    }

    void Update()
    {
        // Move the damage number upwards
        transform.Translate(moveSpeed * Time.deltaTime * Vector3.up);
        transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward,
                        Camera.main.transform.rotation * Vector3.up);
    }

    private void DisableDamageNumber()
    {
        // Disable the damage number and return it to the pool
        gameObject.SetActive(false);
    }

    public void SetDamage(int damage)
    {
        // Set the text of the damage number
        Debug.Log(damage + " tex" );
        damageText.text = damage.ToString();
    }
}
