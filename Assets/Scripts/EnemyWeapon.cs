using UnityEngine;

public class EnemyWeapon : Weapon
{
    protected override bool ShouldShoot => Physics.Raycast(transform.position, transform.forward);
}
