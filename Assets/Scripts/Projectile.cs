using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed = 0.5f;
    [SerializeField] GameObject explosionPrefab;

    public ProjectilePool pool { set; private get; }
    
    IEnumerator Start()
    {
        yield return new WaitForSeconds(5);
        Explode();
    }

    void Update() =>
        transform.position += transform.forward * Time.deltaTime * speed;
    
    void OnCollisionEnter(Collision other) => 
        Explode();

    void Explode()
    {
        Instantiate(explosionPrefab, transform.position, transform.rotation);

        var rv = Instantiate(GetComponentInChildren<RelativisticVisuals>());
        if (rv != null)
        {
            rv.transform.parent = null;
            rv.DestroyRelativistically();
        }

        pool.Return(this);
    }
}
