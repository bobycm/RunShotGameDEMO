using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponInstance
{
    public BulletData data;
    // 各攻擊獨立計時器
    [HideInInspector] public float nextFireTime = 0f;

    public WeaponInstance(BulletData data)
    {
        this.data = data;
    }
}
public class PlayerBulletManager : MonoBehaviour
{
    [Header("PlayerCurrentBullets")]
    public List<WeaponInstance> activeWeapons = new List<WeaponInstance>();

    private AttackSystem attackSystem;

    void Start()
    {
        attackSystem = GetComponent<AttackSystem>();
        if (attackSystem == null)
        {
            Debug.LogError("AttackSystem Missing！");
        }
    }

    // 玩家獲得新武器時調用此函式
    public void AcquireWeapon(BulletData bulletData)
    {
        if (activeWeapons.Exists(w => w.data == bulletData))
        {
            Debug.Log($"已擁有 {bulletData.BulletName}。");
            return;
        }

        activeWeapons.Add(new WeaponInstance(bulletData));
        Debug.Log($"獲得新武器: {bulletData.BulletName}");
    }

}
