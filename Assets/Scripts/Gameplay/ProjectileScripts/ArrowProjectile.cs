using UnityEngine;

public class ArrowProjectile : Projectile
{

    public float knockbackForce = 10f;
    public float knockbackDuration = 0.2f;

    protected override void OnHit(Collider2D other)
    {
        if (other.CompareTag("MainCharacter"))
        {
            PlayerKnockback knockback = other.GetComponent<PlayerKnockback>();
            if (knockback != null)
            {
                knockback.ApplyKnockback(direction * knockbackForce, knockbackDuration);
            }

            Health health = other.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(damage);
            }

            Destroy(gameObject);
        }
        else if (!other.isTrigger)
        {
            Destroy(gameObject); 
        }
    }
}
