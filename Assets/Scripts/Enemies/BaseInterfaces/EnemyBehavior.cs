using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBehavior : ScriptableObject
{
    public abstract void Execute(EnemyAIController controller);
}
