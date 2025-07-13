using UnityEngine;

public class EnemyDeathTracker : MonoBehaviour
{
    [SerializeField] private EnemyType type;


    public void HandleDeath()
    {
        if (StatsManager.Instance != null)
            StatsManager.Instance.AddKill(type);

        if (GameVictoryManager.Instance != null)
            GameVictoryManager.Instance.RegisterEnemyDefeat();

        Destroy(gameObject);
    }
}