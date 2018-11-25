using UnityEngine;

public class EnemyController : AbstractController
{
    [SerializeField] private RelativisticVisuals Target;
    [SerializeField] private int PerceivedLayerIndex = 1;

    private GameObject perceivedTarget;
    
    void Start()
    {
        perceivedTarget = Target.GetAfterImageByLayerIndex(PerceivedLayerIndex);
    }
    
    protected override float Throttle => perceivedTarget.activeSelf ? 1f : 0f;
    
    protected override Vector3 Direction => 
        perceivedTarget.activeSelf 
        ? (perceivedTarget.transform.position - transform.position).normalized
        : transform.forward;

    protected override bool ShouldShoot =>
        perceivedTarget.activeSelf && Vector3.Dot(
            transform.forward,
            (perceivedTarget.transform.position - transform.position).normalized
        ) > 0.75f;

}
