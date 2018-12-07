using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Player
{
   using Assets;
   using System.Collections;
   using System.Collections.Generic;
   using UnityEngine;

   public class Jump : MonoBehaviour
   {
      public Rigidbody Rigidbody;
      MeshRenderer MeshRenderer;
      //public Camera Camera;
      // Start is called before the first frame update
      void Start()
      {
         if (Rigidbody == null)
            Rigidbody = GetComponent<Rigidbody>();
         MeshRenderer = GetComponent<MeshRenderer>();
      }

      public Rigidbody ground = null;
      void OnCollisionEnter(Collision collision)
      {
         //if (collision.impulse.y > .1)
         if (collision.contacts[0].normal.y > .1)
         {
            //Ray ray = new Ray(Rigidbody.position + new Vector3(0, -.5f), Vector3.down);

            if (!grounded)
            {
               ground = collision.rigidbody;
               grounded = true;
               timeSinceGrounded = Time.time;
               var endTime = animStartTime + squashDuration;
               //if (animTime < squashDuration)
               float offset = EasingFunction.EaseOutElastic(animStartTime, endTime, Time.time);

               {
                  MeshRenderer.transform.localScale.Set(1, offset, 1);
               }
            }
            else
            {

               if (Time.time - timeSinceGrounded > RejumpTime)
               {
                  CanJump = true;
               }
            }

         }
         else
         {

         }
      }

      private void OnCollisionExit(Collision collision)
      {
         if (collision.rigidbody == ground)
         {
            grounded = false;
            CanJump = false;
            ground = null;
         }
      }
      public float JumpForce = 200f;
      public float RejumpTime = .2f;

      public float squashDuration = 2f;
      float animStartTime = 0f;
      bool CanJump = false;
      bool grounded;
      float timeSinceGrounded = 0;
      // Update is called once per frame
      void Update()
      {
         if (Input.GetButton("Jump") && CanJump)
         {
            Rigidbody.AddForce(Vector3.up * JumpForce);
            animStartTime = Time.time;
            CanJump = false;
            ground = null;
         }
         //Ray ray = new Ray(Rigidbody.position + new Vector3(0,-.5f), Vector3.down);
         //if (DebugUtils.Raycast(ray, .2f))
         //{
         //   if (!grounded)
         //   {
         //      grounded = true;
         //      timeSinceGrounded = Time.time;
         //      var endTime = animStartTime + squashDuration;
         //      //if (animTime < squashDuration)
         //      float offset = EasingFunction.EaseOutElastic(animStartTime, endTime, Time.time);

         //      {
         //         MeshRenderer.transform.localScale.Set(1, offset, 1);
         //      }
         //   }
         //   else if (Time.time - timeSinceGrounded > RejumpTime)
         //   {
         //      CanJump = true;
         //   }
         //   if (Input.GetButton("Jump") && CanJump)
         //   {
         //      Rigidbody.AddForce(Vector3.up * JumpForce);
         //      animStartTime = Time.time;
         //      CanJump = false;
         //   }
         //}
         //else
         //{
         //   grounded = false;
         //   CanJump = false;
         //}

      }
   }

}
