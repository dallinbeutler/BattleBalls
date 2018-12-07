using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Player
{
   using System.Collections;
   using System.Collections.Generic;
   using UnityEngine;

   public class Swing : MonoBehaviour
   {
      public Rigidbody Rigidbody;
      // Start is called before the first frame update
      void Start()
      {


      }
      public float armlength = 1f;
      // Update is called once per frame
      void Update()
      {
         Vector3 right = new Vector3(.1f, 0f, .1f);
         Ray rayRight = new Ray(Rigidbody.position, transform.rotation.normalized * right);
         if (DebugUtils.Raycast(rayRight, armlength))
         {
            //Debug.DrawRay(rayRight.origin, rayRight.direction, Color.cyan,.1f,true);
            //Debug.DrawLine(rayRight.origin, rayRight.direction, Color.cyan, .1f, true);
         }
         else
         {
            //Debug.DrawRay(rayRight.origin, rayRight.direction, Color.red);
         }
      }

   }

}
