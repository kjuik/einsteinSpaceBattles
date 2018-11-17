using UnityEngine;

public class EnemyMovement: Movement
{
    [SerializeField] private Transform Target;
    
    protected override float Throttle => 1f;
    protected override Vector3 SteeringDirection => Target.position - transform.position;
}
