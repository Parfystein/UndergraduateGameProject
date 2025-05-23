using UnityEngine;

public class PlayerHealthUIConnector : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    private PlayerHUD hud;

    private int lastHealth = -1;

    private void Start()
    {
        hud = FindObjectOfType<PlayerHUD>();
    }

    private void Update()
    {
        if (playerHealth == null || hud == null)
            return;

        int current = playerHealth.CurrentHealth;
        int max = playerHealth.MaxHealth;

        if (current != lastHealth)
        {
            lastHealth = current;
            hud.UpdateHealth(current, max);
        }
    }
}
