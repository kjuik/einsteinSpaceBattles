using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed = 0.5f;

    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * speed;
    }
}
