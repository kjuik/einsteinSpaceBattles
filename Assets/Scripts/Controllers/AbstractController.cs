using UnityEngine;

public abstract class AbstractController : MonoBehaviour
{
    private Weapon weapon;
    private Movement movement;
    
    protected abstract bool ShouldShoot { get; }
    protected abstract float Throttle { get; }
    protected abstract Vector3 Direction { get; }
    
    void Awake()
    {
        weapon = GetComponent<Weapon>();
        movement = GetComponent<Movement>();
    }

    void Update()
    {
        weapon.ShouldShoot = ShouldShoot;
        movement.Direction = Direction;
        movement.Throttle = Throttle;
    }
    
}
