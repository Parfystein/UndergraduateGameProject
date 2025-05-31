using UnityEngine;

public class MagicArrowProjectile : Projectile
{
    [Header("Magic Arrow Settings")]
    private Animator animator;
    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
    }

    protected override void Start()
    {
        rb.velocity = direction * speed;
        Destroy(gameObject, lifetime);
    }
    private void EnableMovement()
    {
        rb.velocity = direction * speed;
    }

    public override void SetDirection(Vector2 dir)
    {
        base.SetDirection(dir);
        rb.velocity = Vector2.zero; 
    }

    protected override void OnHit(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Health>()?.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
