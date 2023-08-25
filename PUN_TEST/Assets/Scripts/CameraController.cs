using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    
    private void Update()
    {
        Vector3 dot = GameObject.Find("Dot").transform.position;
        Vector3 direction = dot - transform.position;
        
        transform.rotation = Quaternion.LookRotation(direction);
    }
}
