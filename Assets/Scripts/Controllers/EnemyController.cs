using UnityEngine;

public class EnemyController : AbstractController
{
    [SerializeField] private RelativisticVisuals Target;
    [SerializeField] private int PerceivedLayerIndex = 1;

    private GameObject perceivedTarget;
    
    protected override void Awake()
    {
        base.Awake();
        perceivedTarget = Target.GetAfterImageByLayerIndex(PerceivedLayerIndex);
    }
    
    protected override float Throttle => 1f;
    
    protected override Vector3 Direction => (perceivedTarget.transform.position - transform.position).normalized;

    protected override bool ShouldShoot =>
        Vector3.Dot(
            transform.forward,
            (perceivedTarget.transform.position - transform.position).normalized
        ) > 0.75f;

}
