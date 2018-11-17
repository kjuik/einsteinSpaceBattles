using UnityEngine;

public class EnemyMovement: Movement
{
    [SerializeField] private Transform Target;
    
    protected override Vector3 GetCurrentSteering()
    {
        return (Target.position - transform.position).normalized;
    }
}
