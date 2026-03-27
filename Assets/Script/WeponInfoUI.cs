using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeponInfoUI : MonoBehaviour
{
    public PlayerBulletManager bulletManager;
    public TextMeshProUGUI levelText;

    [Header("targetWeapon")]
    public BulletData targetWeapon;

    void Update()
    {
        if (bulletManager == null || levelText == null) return;

        if (bulletManager.activeWeapons.Count == 0)
        {
            levelText.text = "X";
            return;
        }

        WeaponInstance weaponToShow = null;

        if (targetWeapon != null)
        {
            weaponToShow = bulletManager.activeWeapons.Find(w => w.data == targetWeapon);
        }
        else
        {
            weaponToShow = bulletManager.activeWeapons[0];
        }

        if (weaponToShow != null)
        {
            levelText.text = "Lv." + weaponToShow.currentBulletsLevel.ToString();
        }
        else
        {
            levelText.text = "X";
        }
    }
}
