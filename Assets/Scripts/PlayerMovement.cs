using UnityEngine;

public class PlayerMovement : Movement
{
   protected override Vector3 GetCurrentSteering()
       {
          return new Vector3(
             Input.GetAxis("Horizontal"),
             0f,
             Input.GetAxis("Vertical")
          );
       }
}
