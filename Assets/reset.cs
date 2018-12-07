using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reset : MonoBehaviour
{
   public Transform starterspot;
   Vector3 resetpoint;
   //Rigidbody Rigidbody;
    // Start is called before the first frame update
    void Start()
    {
      //Rigidbody = GetComponent<Rigidbody>();
      resetpoint = starterspot.position;
    }

    // Update is called once per frame
    void Update()
    {
      if (Input.GetKey(KeyCode.R))
         transform.position = resetpoint - Vector3.up *3;
    }
}
