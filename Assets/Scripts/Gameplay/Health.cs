using UnityEngine;
using System.Collections;
public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;

    [Header("Invulnerability")]
    [SerializeField] private bool useInvulnerability = false;
    [SerializeField] private float invulnerabilityDuration = 1f;
    
    private int currentHealth;
    private float invulnerabilityTimer = 0f;

    private Animator animator;
    [Header("Sound Effects")]
    [SerializeField] private AudioClip hurtSFX;
    private SFXPlayer sfxPlayer;

    private void Awake()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        sfxPlayer = GetComponentInChildren<SFXPlayer>();
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

        if (hurtSFX != null && sfxPlayer != null)
        {
            sfxPlayer.PlaySFX(hurtSFX);
        }

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

        if (CompareTag("MainCharacter"))
        {
            animator.SetTrigger("Death");
            StartCoroutine(ShowGameOverAfterDelay(1.0f));
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private IEnumerator ShowGameOverAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        GameOverManager.Instance.ShowGameOverScreen();
    }

    public int CurrentHealth => currentHealth;
    public int MaxHealth => maxHealth;
}
