using UnityEngine;
using TMPro;

public class DamageNumber : MonoBehaviour
{
    public TextMeshPro damageText;
    public float displayDuration = 1.5f;
    public float moveSpeed = 1f;
    private float scale;

    private void Awake()
    {
        damageText = GetComponent<TextMeshPro>();
        scale = transform.localScale.magnitude;
    }

    private void OnEnable()
    {
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
        transform.localScale = Vector3.one * scale;
        gameObject.SetActive(false);
    }

    public void SetDamage(int damage)
    {
        damageText.text = damage.ToString();
    }
}
