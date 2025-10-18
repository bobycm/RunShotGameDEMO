using UnityEngine;

public class Bullet : MonoBehaviour, IProjectile
{
    private float speed;
    private float lifetime;
    private int damage;
    private Vector3 initialDirection;

    public void Initialize(Vector3 direction, float speed, float lifetime)
    {
        this.speed = speed;
        this.lifetime = lifetime;
        this.initialDirection = direction.normalized;

        transform.rotation = Quaternion.LookRotation(initialDirection);
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
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            IDamageable damageable = other.GetComponent<IDamageable>();

            if (damageable != null)
            {
                damageable.TakeDamage(this.damage);
            }

            Destroy(gameObject);
        }
    }
}