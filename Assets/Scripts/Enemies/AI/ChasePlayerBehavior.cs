using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Behaviors/Chase Player")]
public class ChasePlayerBehavior : EnemyBehavior
{
    public float moveSpeed = 2f;
    public float stopDistance = 0f;

    public override void Execute(EnemyAIController controller)
    {
        if (controller.playerTransform == null) return;

        float distance = Vector2.Distance(controller.transform.position, controller.playerTransform.position);
        if (distance < controller.detectionRadius && distance > stopDistance)
        {
            Vector2 direction = (controller.playerTransform.position - controller.transform.position).normalized;
            controller.transform.position += (Vector3)(direction * moveSpeed * Time.deltaTime);

            SpriteRenderer sr = controller.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.flipX = direction.x < 0f;
            }

            Animator animator = controller.GetComponent<Animator>();
            animator?.SetBool("isWalking", true);
        }
        else
        {
            Animator animator = controller.GetComponent<Animator>();
            animator?.SetBool("isWalking", false);
        }
    }
}
