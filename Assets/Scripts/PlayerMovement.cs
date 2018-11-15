using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float Speed = 5f;
    [SerializeField]
    Transform visuals;

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

        visuals.LookAt(visuals.position + acceleration);
    }
}
