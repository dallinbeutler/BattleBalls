using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_SpeedClouds : MonoBehaviour
{
   // Start is called before the first frame update
   public ParticleSystem Particle;
   public RollMovement Ball;
   void Start()
   {
      Particle = GetComponent<ParticleSystem>();
   }
   public float AngularDustAmount = .1f;
   public float VelocityDustAmount = .1f;
   public float DustMinRequiredAngularVelocity = 8f;
   public float DustMinRequiredVelocity = 8f;
   // Update is called once per frame
   void Update()
   {
      var PartEmission = Particle.emission;

      if (Ball.InAir)
      {
         PartEmission.enabled = false;
      }
      else
      {
         bool torquedust = Ball.RigidBody.angularVelocity.magnitude > DustMinRequiredAngularVelocity;
         bool speedDust = Ball.RigidBody.velocity.magnitude > DustMinRequiredVelocity;
         if (!torquedust)
         {
            
            if (!speedDust)
            {
               PartEmission.enabled = false;
               return;
            }
            PartEmission.rateOverTime = new ParticleSystem.MinMaxCurve(0);
         }
         else
         {
            PartEmission.enabled = true;
            PartEmission.rateOverTime = new ParticleSystem.MinMaxCurve((Ball.RigidBody.angularVelocity.magnitude /*+ Ball.velocity.magnitude*/) * AngularDustAmount);
         }
         if (speedDust)
         {
            PartEmission.enabled = true;
            PartEmission.rateOverDistance = new ParticleSystem.MinMaxCurve((Ball.RigidBody.velocity.magnitude) * VelocityDustAmount);
         }
         else
         {
            PartEmission.rateOverDistance = new ParticleSystem.MinMaxCurve(0);
         }
      }
   }
}
