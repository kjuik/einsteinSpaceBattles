using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed = 0.5f;
    [SerializeField] GameObject explosionPrefab;
    
    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * speed;
    }
    
    void OnCollisionEnter(Collision other)
    {
        //todo relativistic explosion!
        var explosion = Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(explosion.gameObject, explosionPrefab.GetComponentInChildren<ParticleSystem>().main.duration);

        var rv = GetComponentInChildren<RelativisticVisuals>();
        if (rv != null)
        {
            rv.transform.parent = null;
            rv.DestroyRelativistically();
        }
        Destroy(gameObject);
    }
}
