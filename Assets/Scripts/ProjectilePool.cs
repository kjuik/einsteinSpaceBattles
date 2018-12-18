using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class ProjectilePool : MonoBehaviour
{
    [SerializeField] private Projectile ProjectilePrefab;
    private Stack<Projectile> pool = new Stack<Projectile>();
    
    public Projectile Get()
    {
        if (!pool.Any())
        {
            var newProjectile = Instantiate(ProjectilePrefab);
            newProjectile.pool = this;
            pool.Push(newProjectile);
        }

        return pool.Pop();
    }

    public void Return(Projectile p)
    {
        if (p.gameObject.activeSelf)
            p.gameObject.SetActive(false);

        pool.Push(p);
    }
}
