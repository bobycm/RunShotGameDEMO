using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour,IDamageable
{
    public int maxHealth = 10;
    private int currentHealth;
    private GameObject _sourcePrefab;
    public Slider healthBarSlider;

    public void SetPoolSource(GameObject prefab)
    {
        _sourcePrefab = prefab;
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthBar();
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    private void UpdateHealthBar()
    {
        if (healthBarSlider != null)
        {
            healthBarSlider.value = (float)currentHealth / maxHealth;
        }
    }

    private void Die()
    {
        if (_sourcePrefab != null && ObjectPool.Instance != null)
        {
            ObjectPool.Instance.Release(_sourcePrefab, gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
