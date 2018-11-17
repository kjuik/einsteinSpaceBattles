using UnityEngine;

public abstract class Movement : MonoBehaviour
{
    [SerializeField] float Speed = 5f;
    [SerializeField] ParticleSystem exhaustParticles;
    [SerializeField] float particlesStrength = 1f;

    Rigidbody cachedRigidbody;
    
    void Awake()
    {
        cachedRigidbody = GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        var acceleration = GetCurrentSteering() * Speed;

        cachedRigidbody.AddForce(acceleration);
        SetParticlesEmission(acceleration);

        transform.LookAt(transform.position + acceleration);
    }

    protected abstract Vector3 GetCurrentSteering();

    private void SetParticlesEmission(Vector3 acceleration)
    {
        var emission = exhaustParticles.emission;
        emission.rateOverTime = acceleration.sqrMagnitude * particlesStrength;

    }
}
