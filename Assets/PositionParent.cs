using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionParent : MonoBehaviour
{
   // Start is called before the first frame update
   void Start()
   {

   }
   public GameObject parent;
   // Update is called once per frame
   void Update()
   {
   }
   private void LateUpdate()
   {
      gameObject.transform.position = parent.transform.position;
   }
   private void FixedUpdate()
   {
      
   }
}
