using UnityEngine;

public class AttackSystem : MonoBehaviour
{
    private PlayerBulletManager bulletManager;

    public Transform firePoint;

    private void Start()
    {
        bulletManager = GetComponent<PlayerBulletManager>();
        if (bulletManager == null)
        {
            Debug.Log("PlayerBulletManager Missing!");
        }
    }
    void Update()
    {
        if (bulletManager != null)
        {
            for (int i = 0; i < bulletManager.activeWeapons.Count; i++)
            {
                WeaponInstance weapon = bulletManager.activeWeapons[i];

                if (Time.time >= weapon.nextFireTime)
                {
                    Shoot(weapon);

                    if (weapon.data.fireRateInterval > 0)
                    {
                        weapon.nextFireTime = Time.time + weapon.data.fireRateInterval;
                    }
                }
            }
        }
    }

    void Shoot(WeaponInstance weapon)
    {
        BulletData data = weapon.data;

        if (data.bulletPrefab == null)
        {
            Debug.LogError($"Weapon Data '{data.BulletName}' Prefab Missing！");
            return;
        }

        int currentShootCount = data.bulletsPerShot;
        int currentDamage = data.damage;
        int currentPierceCount = data.pierceCount;
        float currentexplosionRadius = data.explosionRadius;

        switch (data.effectType)
        {
            case BulletEffectType.Normal:
                currentDamage += weapon.currentBulletsLevel;
                currentShootCount = weapon.currentBulletsLevel;
                break;
            case BulletEffectType.Piercing:
                currentDamage += weapon.currentBulletsLevel;
                currentPierceCount += weapon.currentBulletsLevel;
                break;
            case BulletEffectType.Explosive:
                currentDamage += weapon.currentBulletsLevel;
                currentexplosionRadius += weapon.currentBulletsLevel;
                break;
        }

        for (int i = 0; i < currentShootCount; i++)
        {
            Vector3 fireDirection = GetFireDirection(data, i, currentShootCount);

            GameObject bulletObject = ObjectPool.Instance.Get(data.bulletPrefab, firePoint.position, Quaternion.LookRotation(fireDirection));

            Bullet bScript = bulletObject.GetComponent<Bullet>();
            if (bScript != null)
            {
                bScript.SetPoolSource(data.bulletPrefab);
            }

            IProjectile projectile = bulletObject.GetComponent<IProjectile>();
            if (projectile != null)
            {
                projectile.Initialize(fireDirection, data.speed, data.lifetime, data.effectType, currentPierceCount, data.explosionRadius);
                
                projectile.SetDamage(currentDamage);
            }
            else
            {
                Debug.LogError($"子彈 Prefab '{data.bulletPrefab.name}' 缺少 IProjectile！");
            }
        }
    }

    private Vector3 GetFireDirection(BulletData data, int shotIndex, int totalBullets)
    {
        if (totalBullets <= 1)
        {
            return firePoint.forward;
        }

        float baseStepDivisor = data.bulletsPerShot > 1 ? (data.bulletsPerShot - 1) : 1f;
        float fixedAngleStep = data.spreadAngle / baseStepDivisor;

        float dynamicTotalSpread = fixedAngleStep * (totalBullets - 1);

        float startAngle = -dynamicTotalSpread / 2f;
        float currentAngle = startAngle + (shotIndex * fixedAngleStep);

        Quaternion spreadRotation = Quaternion.AngleAxis(currentAngle, firePoint.up);
        return spreadRotation * firePoint.forward;
    }
}