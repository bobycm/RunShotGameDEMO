using UnityEngine;

// 讓你可以透過右鍵菜單創建這個資料檔案
[CreateAssetMenu(fileName = "NewBulletData", menuName = "Custom/Bullet Data")]
public class BulletData : ScriptableObject
{
    [Header("Prefab")]
    public GameObject bulletPrefab;

    [Header("Speed")]
    public float speed = 20f;
    public float lifetime = 5f;

    [Header("fireRate")]
    // 1/fr
    public float fireRateInterval = 1.0f;

    [Header("Damage")]
    public int damage = 10;
}