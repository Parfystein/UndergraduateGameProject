using UnityEngine;

public interface IAttackStrategy
{
    void Attack(Vector2 direction, Transform firePoint);
    void Tick(float deltaTime); 
}
