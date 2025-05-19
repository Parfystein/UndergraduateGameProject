using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 8f;
    [SerializeField] LayerMask wallLayerMask; 

    Vector2 moveInput;
    Rigidbody2D playerRigidBody;
    PolygonCollider2D playerCollider;
    RaycastHit2D[] castResults = new RaycastHit2D[5];
    ContactFilter2D wallFilter;

    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<PolygonCollider2D>();

        
        wallFilter = new ContactFilter2D();
        wallFilter.SetLayerMask(wallLayerMask);
        wallFilter.useLayerMask = true;
        wallFilter.useTriggers = false;
    }

    void FixedUpdate()
    {
        TryMove();
    }

    void TryMove()
    {
        if (GetComponent<PlayerKnockback>().IsKnockedBack)
            return;
            
        Vector2 movement = moveInput * speed * Time.fixedDeltaTime;

        if (movement == Vector2.zero)
            return;

        int hits = playerCollider.Cast(
            movement.normalized,
            wallFilter,
            castResults,
            movement.magnitude
        );

        if (hits == 0)
        {
            playerRigidBody.MovePosition(playerRigidBody.position + movement);
        }
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
}
