using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletDmg = 10;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IHealth health = collision.GetComponent<IHealth>();

        if (health != null)
        {
            health.TakeDamage((int)bulletDmg);
            Destroy(gameObject);
        }
    }
}
