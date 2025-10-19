using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour,IDamageable
{
    public int maxHealth = 10;
    private int currentHealth;
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        //���`�ʵe
        Destroy(gameObject);
    }
}
