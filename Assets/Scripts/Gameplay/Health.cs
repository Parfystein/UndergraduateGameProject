using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;

    [Header("Invulnerability")]
    [SerializeField] private bool useInvulnerability = false;
    [SerializeField] private float invulnerabilityDuration = 1f;

    private int currentHealth;
    private float invulnerabilityTimer = 0f;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (useInvulnerability && invulnerabilityTimer > 0f)
        {
            invulnerabilityTimer -= Time.deltaTime;
        }
    }

    public void TakeDamage(int amount)
    {
        if (useInvulnerability && IsInvulnerable())
        {
            return;
        }

        if (currentHealth <= 0) return;

        currentHealth -= amount;
        Debug.Log($"{gameObject.name} took {amount} damage. Current health: {currentHealth}");

        if (useInvulnerability)
        {
            invulnerabilityTimer = invulnerabilityDuration;
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public bool IsInvulnerable()
    {
        return invulnerabilityTimer > 0f;
    }

    private void Die()
    {
        Debug.Log($"{gameObject.name} died.");
        Destroy(gameObject);
    }
}
