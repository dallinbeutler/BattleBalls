using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Player
{
   public class CameraBoom : MonoBehaviour
   {
      public RollMovement RollMovement;
      private Vector3 offset;
      public GameObject Camera;
      public GameObject Target;
      // Start is called before the first frame update
      void Start()
      {
         offset = Camera.transform.position - Target.transform.position;
         RollMovement = GetComponent<RollMovement>();
      }

      // Update is called once per frame
      void Update()
      {

      }

      public float DeadZone = .1f;
      public float LookSpeed = .05f;
      public float AngleSmoothing = .08f;
      public float ControllerSensitivity = 2f;
      public float ControllerAcceleration = .1f;
      float rotation = 0f;
      bool OnPc = false;

      private Vector3 _VelCross;
      //public Vector3 VelCross
      //{
      //   get
      //   {
      //      return VelCross;
      //   }
      //}
      void LateUpdate()
      {
         // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
         Camera.transform.position = Target.transform.position + offset;
         var gn = RollMovement.GroundNormal;
         //transform.rotation.SetLookRotation(new Vector3(gn.x,0,gn.z), gn);
         //transform.Rotate( (transform.localRotation.eulerAngles), Input.GetAxis("Mouse X"), relativeTo: Space.Self);
         //var cross = Vector3.Cross(gn, transform.rotation.eulerAngles);
         var cross = Vector3.Cross(gn, -Camera.transform.right);

         _VelCross = Vector3.Cross(RollMovement.RigidBody.velocity.normalized, -Camera.transform.right);
         if (RollMovement.RigidBody.velocity.magnitude < 1f)
            _VelCross = Vector3.zero;
         //var VelCross = Vector3.Cross(RollMovement.RigidBody.velocity.normalized, gn.normalized);

         //Try and fix acceleration forward tilt being too much

         Debug.DrawRay(RollMovement.RigidBody.position, cross, Color.cyan);
         Debug.DrawRay(RollMovement.RigidBody.position, _VelCross, Color.black);

         if (OnPc)
         {
            var axis = Input.GetAxis("Mouse X");

            if (Mathf.Abs(axis) > DeadZone)
            {

               rotation += axis * .01f;
            }
            rotation %= 359;

         }
         else
         {
            var axis = Input.GetAxis("RightStick X");//Input.GetAxis("Mouse X");
            rotation = Mathf.Lerp(rotation, axis * ControllerSensitivity,ControllerAcceleration);
         }
         //Camera.transform.rotation = Quaternion.Lerp(Camera.transform.rotation, Quaternion.LookRotation(-VelCross, gn), AngleSmoothing/4f);
         //Camera.transform.rotation = Quaternion.Lerp(Camera.transform.rotation, Quaternion.LookRotation( cross.normalized , gn), AngleSmoothing);

         float velocityEffectOnCam;

         if (RollMovement.InAir)
            velocityEffectOnCam = .01f;
         else
            velocityEffectOnCam = .3f;

         Camera.transform.rotation = Quaternion.Lerp(Camera.transform.rotation, Quaternion.LookRotation( cross.normalized - (_VelCross* velocityEffectOnCam), gn), AngleSmoothing);

         Camera.transform.Rotate(Vector3.up, rotation, relativeTo: Space.Self);

      }
   }
}
         //transform.rotation = Quaternion.LookRotation(cross, Vector3.up);
         //rotation = Input.GetAxis("Mouse X");
