using UnityEngine;

// 介面：定義所有投射物 (子彈) 必須提供的能力
public interface IProjectile
{
    void Initialize(Vector3 direction, float speed, float lifetime);

    void SetDamage(int damage);
}