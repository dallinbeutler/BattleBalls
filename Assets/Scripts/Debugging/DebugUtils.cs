using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
   public static class DebugUtils
   {

      public static bool Raycast(Ray ray, float length)
      {
         RaycastHit raycastHit;
         if (Physics.Raycast(ray, out raycastHit, length))
         {
            Debug.DrawLine(ray.origin, raycastHit.point, Color.red);
            Debug.DrawLine(raycastHit.point, raycastHit.point + (raycastHit.normal * .3f), Color.yellow);
            return true;
         }
         else
         {
            Debug.DrawLine(ray.origin, ray.origin + (ray.direction.normalized * length), Color.green);
         }

         return false;

      }
   }
   public interface ICollisionRerouteable
   {
      void OnCollisionEnter(Collision collision);

      void OnCollisionStay(Collision collision);
      void OnCollisionExit(Collision collision);
   }
}
