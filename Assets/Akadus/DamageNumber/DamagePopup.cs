using UnityEngine;

public class DamagePopup : MonoBehaviour
{
    private DamageNumberPool pool;

    private void Awake()
    {
        pool = FindObjectOfType<DamageNumberPool>();
    }

    public void PopupDamage(Transform pos, float damage)
    {
        if(pool == null)
        {
            print("Couldnt find damage pop pool");
            return;
        }
        GameObject go = pool.GetDamageNumber(pos.position);
        go.GetComponent<DamageNumber>().SetDamage(Mathf.CeilToInt(damage));
        go.SetActive(true);
    }
}
