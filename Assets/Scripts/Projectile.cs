using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float lifeTime = 2f;
    private ProjectilePool pool;

    public void Init(ProjectilePool poolRef)
    {
        pool = poolRef;
        Invoke(nameof(ReturnToPool), lifeTime);
    }

    private void ReturnToPool()
    {
        if (pool != null)
        {
            pool.ReturnProjectile(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Parried()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.linearVelocity = -rb.linearVelocity;
        rb.angularVelocity = -rb.angularVelocity;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().RecieveDamage();
        }
        if(other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Actor>().TakeDamage(2, 2);
        }
    }
}
