using UnityEngine;

public class PlayerController : AbstractController
{
    protected override float Throttle => 
        Mathf.Max(
            Mathf.Abs(Input.GetAxis("Horizontal")), 
            Mathf.Abs(Input.GetAxis("Vertical"))
        );
   
    protected override Vector3 Direction => new Vector3(
        Input.GetAxis("Horizontal"),
        0f,
        Input.GetAxis("Vertical")
    );
    
    protected override bool ShouldShoot => Input.GetAxis("Fire") > 0;
}
