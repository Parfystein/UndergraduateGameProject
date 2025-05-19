using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAIController : MonoBehaviour
{
    public EnemyBehavior[] behaviors;

    [HideInInspector] public Transform playerTransform;
    public float detectionRadius = 5f;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("MainCharacter")?.transform;
    }
    
    void Update()
    {
        foreach(var behavior in behaviors)
        {
            behavior.Execute(this);
        }
    }
}
