using UnityEngine;

public class Movement : MonoBehaviour
{
    
    
    [SerializeField] float Speed = 5f;
    [SerializeField] AnimationCurve SteeringCurve;
    [SerializeField] ParticleSystem exhaustParticles;
    [SerializeField] float particlesStrength = 1f;

    protected virtual float Throttle { get; }
    protected virtual Vector3 SteeringDirection { get; }
    
    Rigidbody cachedRigidbody;
    
    void Awake()
    {
        cachedRigidbody = GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        var throttle = Throttle;
        cachedRigidbody.AddForce(transform.forward * Throttle * Speed);
        SetParticlesEmission(throttle);

        if (SteeringDirection != Vector3.zero)
            cachedRigidbody.AddTorque(
                0f,
                SteeringCurve.Evaluate(Vector3.Cross(
                    transform.forward, SteeringDirection
                ).y),
                0f);
    }

    private void SetParticlesEmission(float throttle)
    {
        var emission = exhaustParticles.emission;
        emission.rateOverTime = throttle * particlesStrength;

    }
}
