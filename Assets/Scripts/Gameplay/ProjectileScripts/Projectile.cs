using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Projectile : MonoBehaviour
{
    [Header("Projectile Settings")]
    public float speed = 5f;
    public float lifetime = 3f;
    public int damage = 10;

    protected Rigidbody2D rb;
    protected Vector2 direction;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Start()
    {
        Destroy(gameObject, lifetime);
    }

    public virtual void SetDirection(Vector2 dir)
{
    direction = dir.normalized;
    rb.velocity = direction * speed;

    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    transform.rotation = Quaternion.Euler(0f, 0f, angle);
}


    protected abstract void OnHit(Collider2D other);
    
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
         Debug.Log($"{gameObject.name} triggered by {other.gameObject.name} (tag: {other.tag}, layer: {LayerMask.LayerToName(other.gameObject.layer)})");
        OnHit(other);
    }
}
