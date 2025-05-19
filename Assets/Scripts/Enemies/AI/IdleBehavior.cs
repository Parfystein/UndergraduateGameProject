using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Enemy Behaviors/Idle")]
public class IdleBehavior : EnemyBehavior
{
    public override void Execute(EnemyAIController controller)
    {
        float distance = Vector2.Distance(controller.transform.position, controller.playerTransform.position);
        if (distance < controller.detectionRadius)
        {
           // Debug.Log($"{controller.name} has spotted the player!");
        }
    }
}
