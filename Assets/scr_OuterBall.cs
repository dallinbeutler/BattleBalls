using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_OuterBall : MonoBehaviour
{

   public Rigidbody Rigidbody;

   private Vector3 torque;

   private void Awake()
   {
      //this.rigidbody = this.GetComponent<Rigidbody>();
   }


   public Transform target;
   public Vector3 UpVector = Vector3.up;


   public float alignmentSpeed = .025f;
   public float alignmentDamping = .2f;


   void FixedUpdate()
   {
      // Compute target rotation (align rigidybody's up direction to the normal vector)
      Vector3 normal = Vector3.up;
      Vector3 proj = Vector3.ProjectOnPlane(Rigidbody.transform.forward, normal);
      //Quaternion targetRotation = Quaternion.LookRotation(proj, normal); // The target rotation can be replaced with whatever rotation you want to align to
      Quaternion targetRotation = target.rotation; // The target rotation can be replaced with whatever rotation you want to align to

      Quaternion deltaRotation = Quaternion.Inverse(Rigidbody.transform.rotation) * targetRotation;
      Vector3 deltaAngles = GetRelativeAngles(deltaRotation.eulerAngles);
      Vector3 worldDeltaAngles = Rigidbody.transform.TransformDirection(deltaAngles);

      // alignmentSpeed controls how fast you rotate the body towards the target rotation
      // alignmentDamping prevents overshooting the target rotation
      // Values used: alignmentSpeed = 0.025, alignmentDamping = 0.2
      Rigidbody.AddTorque(alignmentSpeed * worldDeltaAngles - alignmentDamping * Rigidbody.angularVelocity);
   }
      // Convert angles above 180 degrees into negative/relative angles
      Vector3 GetRelativeAngles(Vector3 angles)
      {
         Vector3 relativeAngles = angles;
         if (relativeAngles.x > 180f)
            relativeAngles.x -= 360f;
         if (relativeAngles.y > 180f)
            relativeAngles.y -= 360f;
         if (relativeAngles.z > 180f)
            relativeAngles.z -= 360f;

         return relativeAngles;
      }
}
