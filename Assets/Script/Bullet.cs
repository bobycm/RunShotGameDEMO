using UnityEngine;

public class Bullet : MonoBehaviour, IProjectile
{
    private GameObject _sourcePrefab;
    private float speed;
    private float lifetime;
    private int damage;
    private BulletEffectType effectType;
    private int piercecount;
    private float explosionradius;
    private Vector3 initialDirection;

    public void SetPoolSource(GameObject prefab)
    {
        _sourcePrefab = prefab;
    }
    public void Initialize(Vector3 direction, float speed, float lifetime, BulletEffectType effectType, int pierceCount, float explosionRadius)
    {
        this.speed = speed;
        this.lifetime = lifetime;
        this.initialDirection = direction.normalized;

        transform.rotation = Quaternion.LookRotation(initialDirection);
    }
    private void ReturnToPool()
    {
        if (_sourcePrefab != null)
        {
            ObjectPool.Instance.Release(_sourcePrefab, gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void SetDamage(int damage)
    {
        this.damage = damage;
    }

    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;

        lifetime -= Time.deltaTime;
        if (lifetime <= 0)
        { 
            ExecuteRelease(); 
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            IDamageable damageable = other.GetComponent<IDamageable>();

            if (damageable == null) return;
            switch (this.effectType)
            {
                case BulletEffectType.Normal:
                    damageable.TakeDamage(this.damage);
                    ExecuteRelease();
                    break;
                case BulletEffectType.Piercing:
                    damageable.TakeDamage(this.damage);
                    this.piercecount--;

                    if (this.piercecount <= 0)
                    {
                        ExecuteRelease();
                    }
                    break;
                case BulletEffectType.Explosive:
                    Collider[] hitEnemies = Physics.OverlapSphere(transform.position, this.explosionradius);

                    foreach (var hit in hitEnemies)
                    {
                        if (hit.CompareTag("Enemy"))
                        {
                            IDamageable hitDamageable = hit.GetComponent<IDamageable>();
                            if (hitDamageable != null)
                            {
                                hitDamageable.TakeDamage(this.damage);
                            }
                        }
                    }
                    ExecuteRelease();
                    break;
            }
        }
    }
    private void ExecuteRelease()
    {
        if (_sourcePrefab != null && ObjectPool.Instance != null)
        {
            ObjectPool.Instance.Release(_sourcePrefab, gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}