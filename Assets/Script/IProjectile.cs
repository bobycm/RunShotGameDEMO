using UnityEngine;

public interface IProjectile
{
    void Initialize(Vector3 direction, float speed, float lifetime, BulletEffectType effectType,int pierceCount, float explosionRadius);

    void SetDamage(int damage);
}