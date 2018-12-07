using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RerouteCollisions : MonoBehaviour
{
   public RollMovement Target;

   private void OnCollisionEnter(Collision collision)
   {
      Target.OnCollisionEnter(collision);
   }
   private void OnCollisionStay(Collision collision)
   {
      Target.OnCollisionStay(collision);
   }
   private void OnCollisionExit(Collision collision)
   {
      Target.OnCollisionExit(collision);
   }
}

