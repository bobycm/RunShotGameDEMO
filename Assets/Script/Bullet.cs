using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody rb;
    private float lifeTimer;

    private Vector3 initialVelocity;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Bullet Prefab 缺少 Rigidbody 元件！");
        }
    }

    public void Initialize(Vector3 direction, float speed, float lifetime)
    {
        initialVelocity = direction.normalized * speed;//射擊初速

        lifeTimer = lifetime;

        // 立刻應用速度
        if (rb != null)
        {
            rb.AddForce(initialVelocity, ForceMode.VelocityChange);
        }
    }

    void Update()
    {
        lifeTimer -= Time.deltaTime;
        if (lifeTimer <= 0)
        {
            Destroy(gameObject);
        }
    }
}