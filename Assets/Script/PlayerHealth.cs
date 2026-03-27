using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth Instance { get; private set; }

    public int maxHealth = 5;
    public int currentHealth;

    public UnityEvent<int> OnHealthChanged;
    public UnityEvent OnPlayerDied;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth > maxHealth) currentHealth = maxHealth;
        
        if (OnHealthChanged != null) OnHealthChanged.Invoke(currentHealth);
        Debug.Log("Player Health Changed! Current Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        TakeDamage(-amount);
    }

    private void Die()
    {
        Debug.Log("Player is Dead!");
        FindObjectOfType<playercontroller>().enabled = false;
        GetComponent<AttackSystem>().enabled = false;
        GetComponent<CapsuleCollider>().enabled = false;
        if (OnPlayerDied != null) OnPlayerDied.Invoke();
        
    }

    private void OnTriggerEnter(Collider other)
    {
        HandleCollision(other.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        HandleCollision(collision.gameObject);
    }

    private void HandleCollision(GameObject otherObj)
    {
        IMonsterInstance monster = otherObj.GetComponentInParent<IMonsterInstance>();
        if (monster != null)
        {
            TakeDamage(1);
            PoolItemSource poolSource = otherObj.GetComponentInParent<PoolItemSource>();
            if (poolSource != null && ObjectPool.Instance != null && poolSource.sourcePrefab != null)
            {
                ObjectPool.Instance.Release(poolSource.sourcePrefab, poolSource.gameObject);
            }
            else
            {
                Destroy(otherObj.transform.root.gameObject);
            }
            return;
        }

        ItemWall itemWall = otherObj.GetComponentInParent<ItemWall>();
        if (itemWall != null)
        {
            if (itemWall.currentItemAttribute == ItemAttributeType.Health)
            {
                TakeDamage(-itemWall.currentValue); 
            }
            else if (itemWall.currentItemAttribute != ItemAttributeType.Health)
            {
                PlayerBulletManager bulletManager = GetComponent<PlayerBulletManager>();
                if (bulletManager != null)
                {
                    bulletManager.ApplyWeaponUpgrade(itemWall.currentItemAttribute, itemWall.currentValue);
                }
            }

            itemWall.RecyclePair();
        }
    }
}
