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

    Animator animator;

    private bool facingRight = true;
    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<PolygonCollider2D>();
        animator = GetComponent<Animator>();


        wallFilter = new ContactFilter2D();
        wallFilter.SetLayerMask(wallLayerMask);
        wallFilter.useLayerMask = true;
        wallFilter.useTriggers = false;
    }

    void Update()
    {
        animator.SetBool("isMoving", moveInput != Vector2.zero);
        if (moveInput.x > 0.01f && !facingRight)
        {
            Flip();
        }
        else if (moveInput.x < -0.01f && facingRight)
        {
            Flip();
        }
    }
    void FixedUpdate()
    {
        TryMove();
    }

    void TryMove()
    {
        if (GetComponent<PlayerKnockback>().IsKnockedBack)
            return;
        
        if (animator.GetBool("isAttacking"))
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

    void Flip()
{
    facingRight = !facingRight;
    Vector3 scale = transform.localScale;
    scale.x *= -1;
    transform.localScale = scale;
}

}
