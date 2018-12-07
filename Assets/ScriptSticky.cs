using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptSticky : MonoBehaviour
{
   // Start is called before the first frame update
   void Start()
   {
      rm = GetComponent<RollMovement>();
      if (rm != null)
         Collider = rm.GetComponent<Collider>();
   }
   RollMovement rm;
   public Collider Collider;
   public float torqeIncrease = 6;
   public ConfigurableJointMotion angularMotion;
   private void OnCollisionEnter(Collision collision)
   {
      
      if (collision.collider.attachedRigidbody != null)
      {
         ////collision.collider.gameObject
         ////joint.
         //ConfigurableJoint Joint = gameObject.AddComponent<ConfigurableJoint>();
         //Joint.connectedBody = collision.rigidbody;
         //Joint.anchor = collision.GetContact(0).point;
         //Joint.connectedAnchor = collision.GetContact(0).point;
         ////Joint.xMotion = ConfigurableJointMotion.Limited;
         ////Joint.yMotion = ConfigurableJointMotion.Limited;
         ////Joint.zMotion = ConfigurableJointMotion.Limited;
         //Joint.xMotion = ConfigurableJointMotion.Locked;
         //Joint.yMotion = ConfigurableJointMotion.Locked;
         //Joint.zMotion = ConfigurableJointMotion.Locked;
         //Joint.angularXMotion = angularMotion;
         //Joint.angularYMotion = angularMotion;
         //Joint.angularZMotion = angularMotion;
         ////Joint.enablePreprocessing = false;
         //collision.rigidbody.maxDepenetrationVelocity = .1f;
         ////var sfj = new SoftJointLimit();
         ////sfj.contactDistance = 1f;
         ////Joint.linearLimit = sfj;
         ///
         var other = collision.collider.gameObject;
         other.layer = 8;

         other.transform.parent = this.gameObject.transform;

         var r = other.GetComponent<Rigidbody>();

         Destroy(r);

         if (rm != null)
         {

            rm.MaxTorque += collision.rigidbody.mass;
            rm.MaxTorque += torqeIncrease;
         }

      }

   }


   // Update is called once per frame
   void Update()
   {

   }
}
