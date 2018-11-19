using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float Speed = 5f;
    [SerializeField] float SteeringStrength = 10;
    [SerializeField] ParticleSystem exhaustParticles;
    [SerializeField] float particlesStrength = 1f;

    public float Throttle { set; private get;  }
    public Vector3 Direction { set; private get; }
    
    Rigidbody cachedRigidbody;
    
    void Awake()
    {
        cachedRigidbody = GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        if (Throttle > 0)
        {
            cachedRigidbody.velocity = Vector3.Lerp(
                cachedRigidbody.velocity,
                Throttle * Direction * Speed,
                Mathf.Min(Time.deltaTime * SteeringStrength, 1f));
            
            transform.forward = cachedRigidbody.velocity;
        }

        SetParticlesEmission(Throttle);
    }

    private void SetParticlesEmission(float throttle)
    {
        var emission = exhaustParticles.emission;
        emission.rateOverTime = throttle * particlesStrength;
    }
}
