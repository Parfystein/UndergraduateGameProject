using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float attackCooldown = 0.5f;

    private IAttackStrategy attackStrategy;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();

    }

    private void Start()
    {
       attackStrategy = new ProjectileAttackStrategy(projectilePrefab, attackCooldown);
    }

    private void Update()
    {
        Vector2 direction = GetDirectionToMouse();
        bool isAttacking = Input.GetMouseButton(0);

        animator.SetBool("isAttacking", isAttacking);

        if (isAttacking)
        {
            attackStrategy.Attack(direction, firePoint);
        }

        attackStrategy.Tick(Time.deltaTime);
    }

    private Vector2 GetDirectionToMouse()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f;
        return (mouseWorldPos - firePoint.position).normalized;
    }
}
