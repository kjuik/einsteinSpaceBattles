using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float Speed = 5f;
    [SerializeField] Transform visuals;
    [SerializeField] ParticleSystem exhaustParticles;
    [SerializeField] float particlesStrength = 1f;

    Rigidbody cachedRigidbody;
    
    void Awake()
    {
        cachedRigidbody = GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        var acceleration = new Vector3(
            Input.GetAxis("Horizontal"),
            0f,
            Input.GetAxis("Vertical")
        ) * Speed;

        cachedRigidbody.AddForce(acceleration);
        SetParticlesEmission(acceleration);

        transform.LookAt(visuals.position + acceleration);
    }

    private void SetParticlesEmission(Vector3 acceleration)
    {
        var emission = exhaustParticles.emission;
        emission.rateOverTime = acceleration.sqrMagnitude * particlesStrength;

    }
}
