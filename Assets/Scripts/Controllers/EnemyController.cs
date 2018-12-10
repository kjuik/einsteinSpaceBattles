using UnityEngine;

public class EnemyController : AbstractController
{
    [SerializeField] private RelativisticVisuals Target;
    [SerializeField] private int PerceivedLayerIndex = 1;

    private Vector3? perceivedTargetPosition => Target.GetPositionPerceivedFrom(transform.position);
    
    protected override float Throttle => perceivedTargetPosition.HasValue ? 1f : 0f;
    
    protected override Vector3 Direction =>
        perceivedTargetPosition.HasValue 
        ? (perceivedTargetPosition.Value - transform.position).normalized
        : transform.forward;

    protected override bool ShouldShoot =>
        perceivedTargetPosition.HasValue && Vector3.Dot(
            transform.forward,
            (perceivedTargetPosition.Value - transform.position).normalized
        ) > 0.75f;

}
