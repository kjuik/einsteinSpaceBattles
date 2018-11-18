using UnityEngine;

public class PlayerMovement : Movement
{
   protected override float Throttle => 
      Mathf.Max(
         Mathf.Abs(Input.GetAxis("Horizontal")), 
         Mathf.Abs(Input.GetAxis("Vertical"))
      );
   
   protected override Vector3 SteeringDirection => new Vector3(
      Input.GetAxis("Horizontal"),
      0f,
      Input.GetAxis("Vertical")
   );
}
