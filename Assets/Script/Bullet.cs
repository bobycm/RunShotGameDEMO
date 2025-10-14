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
            Debug.LogError("Bullet Prefab �ʤ� Rigidbody ����I");
        }
    }

    public void Initialize(Vector3 direction, float speed, float lifetime)
    {
        initialVelocity = direction.normalized * speed;//�g����t

        lifeTimer = lifetime;

        // �ߨ����γt��
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