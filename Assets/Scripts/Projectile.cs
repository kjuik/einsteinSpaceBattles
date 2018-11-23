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
        var explosion = Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(explosion.gameObject, explosionPrefab.GetComponentInChildren<ParticleSystem>().main.duration);
        Destroy(gameObject);
    }
}
