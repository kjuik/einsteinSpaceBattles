using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float Speed = 5f;
    [SerializeField] float SteeringStrength = 10;
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
        cachedRigidbody.velocity = Vector3.Lerp(
            cachedRigidbody.velocity, 
            Throttle * SteeringDirection * Speed,
            Mathf.Min(Time.deltaTime * SteeringStrength, 1f));

        transform.forward = cachedRigidbody.velocity;
    }

    private void SetParticlesEmission(float throttle)
    {
        var emission = exhaustParticles.emission;
        emission.rateOverTime = throttle * particlesStrength;

    }
}
