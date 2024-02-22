using UnityEngine;
using UnityEngine.UI;
using TMPro;


namespace Akadus.HealthSystem
{
    public class HealthUI : MonoBehaviour
    {
        private enum TextType
        {
            CurrentDivMax,
            Current
        }

        [SerializeField] private Health _targetHealth;
        [SerializeField] private Image _foregroundBar;
        [SerializeField] private TextMeshProUGUI healthText;

        [Header("Optional Settings")]
        [SerializeField] private TextType _textType = TextType.CurrentDivMax;
        private float maxWidth;

        private void Awake()
        {
            if (_targetHealth == null)
            {
                print("Health is not assigned");
            }
            if (_foregroundBar == null)
            {
                print("Healthbar fill is null");
            }
            else
            {
                maxWidth = _foregroundBar.rectTransform.sizeDelta.x;
            }

            _targetHealth.OnHealthChanged += UpdateUI;
            UpdateUI();
        }

        private void UpdateUI()
        {
            if(GetComponent<MoreMountains.Tools.MMProgressBar>() != null)
            {
                GetComponent<MoreMountains.Tools.MMProgressBar>().UpdateBar(_targetHealth.CurrentHealth, 0f, _targetHealth.MaxHealth);
                return;
            }

            float healthPercentage = Mathf.Clamp01(_targetHealth.CurrentHealth / _targetHealth.MaxHealth);
            float newWidth = maxWidth * healthPercentage;

            // Update the foreground bar size
            _foregroundBar.rectTransform.sizeDelta = new Vector2(newWidth, _foregroundBar.rectTransform.sizeDelta.y);

            // Update the health text with integer values
            if (healthText != null)
            {
                int roundedCurrentHealth = Mathf.RoundToInt(_targetHealth.CurrentHealth);
                int roundedMaxHealth = Mathf.RoundToInt(_targetHealth.MaxHealth);


                if (_textType == TextType.Current)
                {
                    healthText.text = $"{roundedCurrentHealth}";
                }
                else if (_textType == TextType.CurrentDivMax)
                {
                    healthText.text = $"{roundedCurrentHealth} / {roundedMaxHealth}";
                }
            }

        }


    }
}
