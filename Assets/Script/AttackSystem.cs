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
                // �T�O fireRateInterval �����s�A�קK�L���g��
                if (currentBulletData.fireRateInterval > 0)
                {
                    nextFireTime = Time.time + currentBulletData.fireRateInterval;
                }
                else
                {
                    nextFireTime = Time.time + 0.001f;//�]�w���p���j�קK�L���`��
                }
            }
        }
    }

    void Shoot()
    {
        if (currentBulletData.bulletPrefab == null)
        {
            Debug.LogError("BulletData ���S�����w Prefab�I");
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