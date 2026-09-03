using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 50f;
    public float lifetime = 5f;

    void Start()
    {
        Destroy(gameObject, lifetime); // Destroy bullet after time
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject); // Despawn enemy
        }

        Destroy(gameObject); // Destroy bullet on impact
    }
}