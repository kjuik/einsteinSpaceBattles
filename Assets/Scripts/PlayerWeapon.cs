using UnityEngine;

public class PlayerWeapon : Weapon
{
    protected override bool ShouldShoot => Input.GetAxis("Fire") > 0;
}
