using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pladddler : MonoBehaviour
{

    public float moveSpeed = 5f;
    public Rigidbody rb;

    Vector3 movement;
    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.z = Input.GetAxisRaw("Vertical");
        if (Input.GetButton("Jump"))
            movement.y = 1;
        else
            movement.y = 0;
        movement.Normalize();
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
