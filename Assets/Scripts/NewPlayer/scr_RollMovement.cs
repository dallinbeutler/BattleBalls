using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Assets.Scripts;

public class scr_RollMovement : MonoBehaviour
{
   private Rigidbody Rigidbody;

   public bool InAir = true;
   public Vector3 Forward = Vector3.forward;

   Vector3 GroundNormal;
   Vector3 AdjustedNormal;
   Vector3 GravityDirecton;
   public Quaternion ForwardRotation = Quaternion.identity;

   public float NoMovementTorqueDrag = .8f;
   public float GroundTorqueDrag = .05f;
   public float TorqueSpeed = 10f;

   void Start()
   {
      Rigidbody = GetComponent<Rigidbody>();
      Rigidbody.maxAngularVelocity = 30f;
   }

   private void OnCollisionEnter(Collision collision)
   {
      InAir = false;

   }

   private void OnCollisionStay(Collision collision)
   {
   }
   private void OnCollisionExit(Collision collision)
   {
      if (collision.contactCount < 1)
      {
         InAir = true;
      }
   }

   void Update()
   {
      
   }

   private void FixedUpdate()
   {
      float inputX = Input.GetAxis("Horizontal");
      float inputY = Input.GetAxis("Vertical");
      //Decelleration on no movement input
      if ((inputX + inputY)< .1)
      {
         Rigidbody.AddForce(-Rigidbody.velocity * .5f);
         Rigidbody.angularDrag = NoMovementTorqueDrag;
         //CurrentGroundTorque = Mathf.Lerp(CurrentGroundTorque, MinGroundTorque, .01f);
      }
      else
      {
         Rigidbody.angularDrag = GroundTorqueDrag;
         //CurrentGroundTorque = Mathf.Lerp(CurrentGroundTorque, MaxSpeed, .01f);
      }

      
      Vector3 torqueDirection = ForwardRotation * new Vector3(inputY, 0, -inputX);
      Rigidbody.AddTorque(torqueDirection * TorqueSpeed );
      
   }

   Vector3 AdjustForGround(Collision collision, Vector3 CurrentNormal, float influence)
   {
      Vector3 hitAverage = Vector3.zero;
      collision.contacts.ToList().ForEach(x => hitAverage += x.normal);
      hitAverage /= collision.contacts.Length;

      Vector3 outV = Vector3.Lerp(CurrentNormal, hitAverage, influence);

      return outV;
   }

   //simple mental note that this is how it's done
   //Vector3 DirectionRelativeToRotation(Quaternion rotation, Vector3 direction)
   //{
   //   return rotation * direction;
   //}
}
