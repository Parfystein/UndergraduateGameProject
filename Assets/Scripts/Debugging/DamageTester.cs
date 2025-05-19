using UnityEngine;

public class DamageTester : MonoBehaviour
{
    public Health health;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            health.TakeDamage(10);
        }
    }
}
