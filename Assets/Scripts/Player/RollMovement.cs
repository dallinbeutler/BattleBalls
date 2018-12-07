using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollMovement : MonoBehaviour
{
   public Rigidbody RigidBody;
   public Camera Camera;
   // Start is called before the first frame update
   void Start()
   {

   }
   public float MinGroundTorque = 20;
   public float MaxTorque = 30;
   public float CurrentGroundTorque = 20;
   public float airSpeed = 2;
   public float BrakingPower = .05f;
   public float MaxSpeed = 200;
   public float MaxAngular = 30;
   public Vector3 _GroundNormal = Vector3.up;
   public Vector3 GroundNormal
   {
      get
      {
         return _GroundNormal;
      }
   }

   public void OnCollisionEnter(Collision collision)
   {
      if (collision.rigidbody == null)
         _AdjustForGround(collision,.5f);
   }
   float AirTime = -1f;
   public void OnCollisionStay(Collision collision)
   {
      _AdjustForGround(collision,.2f);

   }
   void _AdjustForGround(Collision collision, float influence)
   {
      var point = collision.GetContact(0).normal;
      //if (point.y > 0)
      //{
      _GroundNormal = Vector3.Lerp(_GroundNormal, point, influence);
      Debug.DrawRay(transform.position + Vector3.up, GroundNormal, Color.green);
      inAir = false;
      //}
      //else
      //{
      //   Debug.DrawRay(transform.position + Vector3.up, GroundNormal, Color.red);

      //}
   }

   public void OnCollisionExit(Collision collision)
   {
      if (collision.contactCount < 1)
      {
         inAir = true;
      }
   }
   bool inAir = false;
   public bool InAir
   {
      get
      {
         return inAir;
      }
   }
   public float FallingNormalCorrectionRate = .02f;
   public float LerpTowardVelocityRate = .005f;
   // Update is called once per frame
   void FixedUpdate()
   {
      RigidBody.maxAngularVelocity = MaxAngular;
      Vector3 direction = new Vector3(Input.GetAxis("Vertical"), 0, -Input.GetAxis("Horizontal"));
      if (direction.magnitude < .1)
      {
         RigidBody.AddForce(-RigidBody.velocity * .5f);
         RigidBody.angularDrag = .8f;
         CurrentGroundTorque = Mathf.Lerp(CurrentGroundTorque, MinGroundTorque, .01f);
      }
      else
      {
         RigidBody.angularDrag = .05f;
         CurrentGroundTorque = Mathf.Lerp(CurrentGroundTorque, MaxSpeed, .01f);
      }

      var movementRelativeToCam = GetAxisDirectionRelativeToCam(Camera.transform.rotation, RigidBody.velocity);
      Vector3 AirMoveDirection = GetAxisDirectionRelativeToCam(Camera.transform.rotation, new Vector3(-direction.z, 0, direction.x));
      bool oppositeDirection = false;
      var angleDifference = Mathf.Abs(Vector3.Angle(AirMoveDirection, movementRelativeToCam)) - 90;
      if (angleDifference > 0)
      {
         oppositeDirection = true;
      }
      if (inAir)
      {
         AirTime += Time.smoothDeltaTime;

         AirMoveDirection.Scale(new Vector3(airSpeed, airSpeed, airSpeed));
         RigidBody.AddForce(AirMoveDirection, ForceMode.Acceleration);
         //if (AirTime > .4f)
            //_GroundNormal = Vector3.Lerp(GroundNormal, Vector3.up, AirTime * FallingNormalCorrectionRate);
            _GroundNormal = Vector3.Lerp(GroundNormal, Vector3.up, AirTime * FallingNormalCorrectionRate);
         //else
         //{
         //   Debug.Log("Delay!");
         //}
      }
      else
      {
         AirTime = -1f;

         Vector3 torqueDirection = GetAxisDirectionRelativeToCam(Camera.transform.rotation, direction);
         torqueDirection.Scale(new Vector3(CurrentGroundTorque, CurrentGroundTorque, CurrentGroundTorque));
         RigidBody.AddTorque(torqueDirection, ForceMode.Acceleration);
         //Apply ficticious braking for a better handling feel
         if (oppositeDirection)
         {
            var PowerToApply = angleDifference * BrakingPower;
            Debug.Log("PowerToApply: " + PowerToApply);
            AirMoveDirection.Scale(new Vector3(PowerToApply, PowerToApply, PowerToApply));
            RigidBody.AddForce(AirMoveDirection, ForceMode.Force);

         }
         //try doing air move crap anyways
         //Vector3 AirMoveDirection = GetAxisDirectionRelativeToCam(Camera.transform.rotation, new Vector3(-direction.z, 0, direction.x));
         //AirMoveDirection.Scale(new Vector3(airSpeed, airSpeed, airSpeed));
         //RigidBody.AddForce(AirMoveDirection, ForceMode.Acceleration);
      }

   }

   Vector3 GetAxisDirectionRelativeToCam(Quaternion rotation, Vector3 direction)
   {
      Vector3 answer = rotation * direction;
      return answer;
   }
}
