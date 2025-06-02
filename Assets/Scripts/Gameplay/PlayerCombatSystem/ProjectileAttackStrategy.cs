using UnityEngine;

public class ProjectileAttackStrategy : IAttackStrategy
{
    private GameObject projectilePrefab;
    private float cooldown;
    private float timer;

    public ProjectileAttackStrategy(GameObject prefab, float attackCooldown)
    {
        projectilePrefab = prefab;
        cooldown = attackCooldown;
        timer = 0f;
    }

    public void Attack(Vector2 direction, Transform firePoint)
    {
        if (timer > 0f) return;

        GameObject projectile = GameObject.Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        projectile.GetComponent<Projectile>().SetDirection(direction);
        timer = cooldown;
    }

    public void Tick(float deltaTime)
    {
        timer -= deltaTime;
    }
}
