using UnityEngine;

public class PlayerMovement : Movement
{
   protected override float Throttle => SteeringDirection.magnitude;
   protected override Vector3 SteeringDirection => new Vector3(
      Input.GetAxis("Horizontal"),
      0f,
      Input.GetAxis("Vertical")
   );
}
