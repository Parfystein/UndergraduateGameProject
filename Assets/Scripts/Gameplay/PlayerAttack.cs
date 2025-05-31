using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform firePoint;

    [Header("Attack Settings")]
    [SerializeField] private float attackInterval = 0.5f; 

    private Animator animator;
    private float attackTimer = 0f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        bool isHoldingAttack = Input.GetMouseButton(0);
        animator.SetBool("isAttacking", isHoldingAttack);

        if (isHoldingAttack)
        {
            attackTimer -= Time.deltaTime;

            if (attackTimer <= 0f)
            {
                FireProjectile();
                attackTimer = attackInterval;
            }
        }
        else
        {
            attackTimer = 0f; 
        }

        
    }

    private void FireProjectile()
{
    Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    mouseWorldPos.z = 0f;

    Vector2 direction = (mouseWorldPos - firePoint.position).normalized;

    GameObject arrow = Instantiate(projectile, firePoint.position, Quaternion.identity);
    arrow.GetComponent<MagicArrowProjectile>().SetDirection(direction);


}
}
