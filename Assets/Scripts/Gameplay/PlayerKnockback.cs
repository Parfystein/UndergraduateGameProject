using UnityEngine;

public class PlayerKnockback : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D col;

    private bool isKnockedBack = false;
    private Vector2 knockbackVelocity;
    private float knockbackTimer = 0f;

    [SerializeField] private LayerMask wallLayer;
    private RaycastHit2D[] castResults = new RaycastHit2D[5];

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    public void ApplyKnockback(Vector2 force, float duration)
    {
        isKnockedBack = true;
        knockbackVelocity = force;
        knockbackTimer = duration;
    }

    private void FixedUpdate()
    {
        if (isKnockedBack)
        {
            Vector2 movement = knockbackVelocity * Time.fixedDeltaTime;

            int hits = col.Cast(
                movement.normalized,
                new ContactFilter2D
                {
                    layerMask = wallLayer,
                    useLayerMask = true,
                    useTriggers = false
                },
                castResults,
                movement.magnitude
            );

            if (hits == 0)
            {
                rb.MovePosition(rb.position + movement);
            }
            else
            {
                isKnockedBack = false;
            }

            knockbackTimer -= Time.fixedDeltaTime;
            if (knockbackTimer <= 0f)
            {
                isKnockedBack = false;
            }
        }
    }

    public bool IsKnockedBack => isKnockedBack;
}
