using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponInstance
{
    public BulletData data;
    public int currentBulletsLevel;
    
    [HideInInspector] public float nextFireTime = 0f;

    public WeaponInstance(BulletData data)
    {
        this.data = data;
        this.currentBulletsLevel = 1;
    }
}

[System.Serializable]
public struct ItemAttributeToWeapon
{
    public ItemAttributeType attrType;
    public BulletData weaponData;
}

public class PlayerBulletManager : MonoBehaviour
{
    [Header("PlayerCurrentBullets")]
    public List<WeaponInstance> activeWeapons = new List<WeaponInstance>();
    
    [Header("Weapon Mappings")]
    public ItemAttributeToWeapon[] weaponMappings;

    private AttackSystem attackSystem;

    void Start()
    {
        attackSystem = GetComponent<AttackSystem>();
        if (attackSystem == null)
        {
            Debug.LogError("AttackSystem Missing！");
        }
    }

    public void ApplyWeaponUpgrade(ItemAttributeType attrType, int amount)
    {
        BulletData targetData = null;
        if (weaponMappings != null)
        {
            foreach (var mapping in weaponMappings)
            {
                if (mapping.attrType == attrType)
                {
                    targetData = mapping.weaponData;
                    break;
                }
            }
        }

        if (targetData == null) return;

        WeaponInstance existingWeapon = activeWeapons.Find(w => w.data == targetData);

        if (existingWeapon != null)
        {
            existingWeapon.currentBulletsLevel += amount;
            
            if (existingWeapon.currentBulletsLevel <= 0)
            {
                activeWeapons.Remove(existingWeapon);
                Debug.Log($"失去武器: {targetData.BulletName}");
            }
            else
            {
                Debug.Log($"更新武器 {targetData.BulletName} 等級 + {existingWeapon.currentBulletsLevel}");
            }
        }
        else
        {
            if (amount > 0)
            {
                WeaponInstance newWeapon = new WeaponInstance(targetData);
                activeWeapons.Add(newWeapon);
                Debug.Log($"獲得新武器: {targetData.BulletName} 等級為: {amount}");
            }
        }
    }

    public void AcquireWeapon(BulletData bulletData)
    {
        if (activeWeapons.Exists(w => w.data == bulletData)) return;
        activeWeapons.Add(new WeaponInstance(bulletData));
    }
}
