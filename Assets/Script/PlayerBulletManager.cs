using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponInstance
{
    public BulletData data;
    // �U�����W�߭p�ɾ�
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
            Debug.LogError("AttackSystem Missing�I");
        }
    }

    // ���a��o�s�Z���ɽեΦ��禡
    public void AcquireWeapon(BulletData bulletData)
    {
        if (activeWeapons.Exists(w => w.data == bulletData))
        {
            Debug.Log($"�w�֦� {bulletData.BulletName}�C");
            return;
        }

        activeWeapons.Add(new WeaponInstance(bulletData));
        Debug.Log($"��o�s�Z��: {bulletData.BulletName}");
    }

}
