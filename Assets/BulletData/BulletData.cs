using UnityEngine;

// ���A�i�H�z�L�k����Ыسo�Ӹ���ɮ�
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