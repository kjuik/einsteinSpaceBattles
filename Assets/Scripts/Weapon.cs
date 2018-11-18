﻿using UnityEngine;

public class Weapon : MonoBehaviour
{
    //todo pooling
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private float shotFrequency;

    float lastShotTimestamp;
    
    bool CanShoot=> lastShotTimestamp + shotFrequency <= Time.time;

    protected virtual bool ShouldShoot { get; }

    void Update()
    {
        if (CanShoot && ShouldShoot)
            Shoot();
    }

    private void Shoot()
    {
        Instantiate(projectilePrefab, 
                    transform.position, 
                    transform.rotation).transform.forward = transform.forward;

        lastShotTimestamp = Time.time;
    }

       
}