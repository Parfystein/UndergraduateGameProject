using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        bool isAttacking = Input.GetMouseButton(0);
        animator.SetBool("isAttacking", isAttacking);
    }
}
