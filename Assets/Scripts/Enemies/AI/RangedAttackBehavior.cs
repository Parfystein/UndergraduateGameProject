using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Enemy Behaviors/Ranged Attack")]
public class RangedAttackBehavior : EnemyBehavior
{
    public float shootCooldown = 2f;
    public float initialShootDelay = 0.5f;
    public GameObject projectilePrefab;
    public string firePointName = "ShootPoint";

    private readonly Dictionary<GameObject, float> shootTimers = new();
    private readonly Dictionary<GameObject, bool> hasShotOnce = new();

    public override void Execute(EnemyAIController controller)
    {
        if (controller.playerTransform == null || projectilePrefab == null) return;

        float distance = Vector2.Distance(controller.transform.position, controller.playerTransform.position);
        float stopDistance = GetRangeFromChaseBehavior(controller);

        if (!shootTimers.ContainsKey(controller.gameObject))
        {
            shootTimers[controller.gameObject] = initialShootDelay;
            hasShotOnce[controller.gameObject] = false;
        }

        shootTimers[controller.gameObject] -= Time.deltaTime;

        Animator animator = controller.GetComponent<Animator>();
        SpriteRenderer sr = controller.GetComponent<SpriteRenderer>();

        if (distance <= stopDistance)
        {
            animator?.SetBool("isWalking", false);
            animator?.SetBool("isShooting", true);

            Vector2 direction = (controller.playerTransform.position - controller.transform.position).normalized;
            if (sr != null)
                sr.flipX = direction.x < 0f;

            if (shootTimers[controller.gameObject] <= 0f)
            {
                ShootProjectile(controller, direction);

                shootTimers[controller.gameObject] = shootCooldown;
                hasShotOnce[controller.gameObject] = true;
            }
        }
        else
        {
            animator?.SetBool("isShooting", false);

            hasShotOnce[controller.gameObject] = false;
            shootTimers[controller.gameObject] = initialShootDelay;
        }
    }

    private void ShootProjectile(EnemyAIController controller, Vector2 direction)
    {
        Transform firePoint = controller.transform.Find(firePointName);
        if (firePoint == null) firePoint = controller.transform;

        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

        Projectile proj = projectile.GetComponent<Projectile>();
        if (proj != null)
        {
            proj.SetDirection(direction);
        }


    }

    private float GetRangeFromChaseBehavior(EnemyAIController controller)
    {
        foreach (var behavior in controller.behaviors)
        {
            if (behavior is ChasePlayerBehavior chase)
                return chase.range;
        }
        return 4f; 
    }
}
