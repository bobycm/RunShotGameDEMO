using UnityEngine;

public class ObjectDestroyLine : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // If a monster hits the destroy line, it means it was missed, so player takes damage
        IMonsterInstance monster = other.GetComponentInParent<IMonsterInstance>();
        if (monster != null)
        {
            if (PlayerHealth.Instance != null)
            {
                PlayerHealth.Instance.TakeDamage(1);
            }
        }

        ItemWall itemWall = other.GetComponentInParent<ItemWall>();
        if (itemWall != null && itemWall.gameObject.activeInHierarchy)
        {
            itemWall.RecyclePair();
            return; // 已經被 RecyclePair 處理掉，直接返回
        }

        PoolItemSource poolSource = other.GetComponentInParent<PoolItemSource>();
        if (poolSource != null && poolSource.sourcePrefab != null)
        {
            if (poolSource.gameObject.activeInHierarchy)
            {
                if (ObjectPool.Instance != null)
                {
                    ObjectPool.Instance.Release(poolSource.sourcePrefab, poolSource.gameObject);
                }
                else
                {
                    Destroy(poolSource.gameObject);
                }
            }
        }
        else if (other.gameObject.activeInHierarchy)
        {
            Destroy(other.transform.root.gameObject);
        }
    }
}
