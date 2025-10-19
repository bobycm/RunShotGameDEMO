using UnityEngine;

public class AttackSystem : MonoBehaviour
{
    private PlayerBulletManager bulletManager;

    public Transform firePoint;

    private float nextFireTime = 0f;

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
            for (int i = 0; i < bulletManager.activeWeapons.Count; i++)//All bullets type
            {
                WeaponInstance weapon = bulletManager.activeWeapons[i];

                // 獨立檢查每種武器的冷卻時間
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
            Debug.LogError($"Weapon Data '{data.BulletName}' 沒有指定 Prefab！");
            return;
        }

        for (int i = 0; i < data.bulletsPerShot; i++)
        {
            Vector3 fireDirection = GetFireDirection(data, i);

            GameObject bulletObject = Instantiate(
                data.bulletPrefab,
                firePoint.position,
                Quaternion.LookRotation(fireDirection) // 讓子彈朝向移動方向
            );

            IProjectile projectile = bulletObject.GetComponent<IProjectile>();

            if (projectile != null)
            {
                projectile.Initialize(fireDirection, data.speed, data.lifetime);
                projectile.SetDamage(data.damage);
            }
            else
            {
                Debug.LogError($"子彈 Prefab '{data.bulletPrefab.name}' 缺少 IProjectile！");
            }
        }
    }

    private Vector3 GetFireDirection(BulletData data, int shotIndex)
    {
        if (data.bulletsPerShot <= 1 || data.spreadAngle == 0f)
        {
            return firePoint.forward;//非複數彈直接射擊
        }

        // 計算每發子彈的偏移角度
        float startAngle = -data.spreadAngle / 2f;
        float angleStep = data.spreadAngle / (data.bulletsPerShot - 1);
        float currentAngle = startAngle + (shotIndex * angleStep);

        // 將方向旋轉
        Quaternion spreadRotation = Quaternion.AngleAxis(currentAngle, firePoint.up);
        return spreadRotation * firePoint.forward;
    }
}