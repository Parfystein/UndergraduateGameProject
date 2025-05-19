using UnityEngine;

public class DamageOnTouch : MonoBehaviour
{
    [SerializeField] private int damageAmount = 10;
    [SerializeField] private float knockbackForce = 10f;
    [SerializeField] private float knockbackDuration = 0.2f;
    [SerializeField] private Transform knockbackOrigin;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("MainCharacter"))
        {
            Health health = other.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(damageAmount);
            }

            PlayerKnockback knockback = other.GetComponent<PlayerKnockback>();
            if (knockback != null)
            {
                 Vector2 origin = knockbackOrigin != null ? knockbackOrigin.position : transform.position;
                Vector2 direction = (other.transform.position - (Vector3)origin).normalized;
                knockback.ApplyKnockback(direction * knockbackForce, knockbackDuration);
            }
        }
    }
}
