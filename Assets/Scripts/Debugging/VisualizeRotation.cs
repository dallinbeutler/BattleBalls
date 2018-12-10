using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualizeRotation : MonoBehaviour
{
   public Quaternion StartRotation;
    // Start is called before the first frame update
    void Start()
    {
      StartRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
