using UnityEngine;

// �����G�w�q�Ҧ���g�� (�l�u) �������Ѫ���O
public interface IProjectile
{
    void Initialize(Vector3 direction, float speed, float lifetime);

    void SetDamage(int damage);
}