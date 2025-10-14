using UnityEngine;

public class AttackSystem : MonoBehaviour
{
    public BulletData currentBulletData;

    public Transform firePoint;

    private float nextFireTime = 0f;

    void Update()
    {
        if (Time.time >= nextFireTime)
        {
            if (currentBulletData != null)
            {
                Shoot();
                // 確保 fireRateInterval 不為零，避免無限射擊
                if (currentBulletData.fireRateInterval > 0)
                {
                    nextFireTime = Time.time + currentBulletData.fireRateInterval;
                }
                else
                {
                    nextFireTime = Time.time + 0.001f;//設定極小間隔避免無限循環
                }
            }
        }
    }

    void Shoot()
    {
        if (currentBulletData.bulletPrefab == null)
        {
            Debug.LogError("BulletData 中沒有指定 Prefab！");
            return;
        }

        GameObject bulletObject = Instantiate(
            currentBulletData.bulletPrefab,
            firePoint.position,
            firePoint.rotation
        );

        Bullet bulletScript = bulletObject.GetComponent<Bullet>();

        if (bulletScript != null)
        {
            bulletScript.Initialize(
                firePoint.forward,
                currentBulletData.speed,
                currentBulletData.lifetime
            );
        }
    }
}