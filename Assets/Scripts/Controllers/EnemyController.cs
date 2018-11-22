using UnityEngine;

public class EnemyController : AbstractController
{
    [SerializeField] private Transform Target;

    protected override float Throttle => 1f;
    
    protected override Vector3 Direction => (Target.position - transform.position).normalized;
    
    protected override bool ShouldShoot => Physics.Raycast(transform.position, transform.forward);

}
