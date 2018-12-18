using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    //todo pooling
    [SerializeField] private ProjectilePool projectilePool;
    [SerializeField] private float shotFrequency;
    [SerializeField] private float delay = 0f;
    [SerializeField] private List<Transform> muzzles;
    private int lastMuzzleIndex;    

    float lastShotTimestamp;
    
    bool CanShoot=> lastShotTimestamp + shotFrequency <= Time.time;

    public bool ShouldShoot { set; private get; }

    void Awake()
    {
        lastShotTimestamp = Time.time - shotFrequency + delay;
    }
    
    void Update()
    {
        if (CanShoot && ShouldShoot)
            Shoot();
    }

    private void Shoot()
    {
        var projectile = projectilePool.Get();
        projectile.transform.position = muzzles[lastMuzzleIndex].position;
        projectile.transform.forward = transform.forward;

        lastShotTimestamp = Time.time;
        lastMuzzleIndex = (lastMuzzleIndex < muzzles.Count - 1)
            ? lastMuzzleIndex + 1
            : 0;
    }

       
}
