using UnityEngine;
using UnityEngine.UI; // Remember to import this for UI elements like Image!

public class HealthUI : MonoBehaviour
{
    [Header("UI")]
    public Image[] heartImages;
    
    [Header("Sprite")]
    [Tooltip("fullheart")]
    public Sprite fullHeartSprite;
    [Tooltip("brokenheart")]
    public Sprite brokenHeartSprite;

    private void Start()
    {
        if (PlayerHealth.Instance != null)
        {
            PlayerHealth.Instance.OnHealthChanged.AddListener(UpdateHealthUI);
            
            UpdateHealthUI(PlayerHealth.Instance.currentHealth);
        }
        else
        {
            Debug.LogWarning("PlayerHealth.Instance Missing！");
        }
    }

    private void OnDestroy()
    {
        if (PlayerHealth.Instance != null)
        {
            PlayerHealth.Instance.OnHealthChanged.RemoveListener(UpdateHealthUI);
        }
    }

    public void UpdateHealthUI(int currentHealth)
    {
        for (int i = 0; i < heartImages.Length; i++)
        {
            if (i < currentHealth)
            {
                if (heartImages[i] != null)
                    heartImages[i].sprite = fullHeartSprite;
            }
            else
            {
                if (heartImages[i] != null)
                    heartImages[i].sprite = brokenHeartSprite;
            }
        }
    }
}
