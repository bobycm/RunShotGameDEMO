using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour, IDamageable
{
    private float speed = 2;
    private int health = 10;
    private int currentHealth;

    void Update()
    {
        transform.position -= transform.forward * speed * Time.deltaTime;
    }
    private void Start()
    {
        currentHealth = health;
    }
    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
