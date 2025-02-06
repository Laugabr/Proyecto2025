using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BasicMovement : MonoBehaviour
{
    private float moveSpeed = 10f;
    private float speedX, speedY;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        speedX = Input.GetAxisRaw("Horizontal") * moveSpeed;
        speedY = Input.GetAxisRaw("Vertical") * moveSpeed;
        rb.linearVelocity = new Vector2(speedX,speedY );
        if (Input.GetAxisRaw("Horizontal") != 0 && Input.GetAxisRaw("Vertical") != 0)
        {
            rb.linearVelocity = new Vector2(speedX/1.8f,speedY/1.8f);
        }
    }


}

