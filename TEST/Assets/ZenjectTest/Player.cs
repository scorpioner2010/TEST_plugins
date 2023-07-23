using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    private Rigidbody rb;
    private Vector2 movement;


    private void Awake()
    {

        rb = GetComponent<Rigidbody>();
    }


    private void Update()
    {
        movement = Vector2.zero;
        
        if (Input.GetKey(KeyCode.W))
        {
            movement = new Vector2(0, 1);
        }
        else if(Input.GetKey(KeyCode.S))
        {
            movement = new Vector2(0, -1);
        }
        
        if (Input.GetKey(KeyCode.D))
        {
            movement = new Vector2(1, 0);
        }
        else if(Input.GetKey(KeyCode.A))
        {
            movement = new Vector2(-1, 0);
        }
        
        
        
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector3(movement.x* Time.fixedDeltaTime * speed, rb.velocity.y, movement.y* Time.fixedDeltaTime * speed) ;
    }
}
