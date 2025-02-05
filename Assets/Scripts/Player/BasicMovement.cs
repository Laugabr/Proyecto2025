using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BasicMovement : MonoBehaviour
{
    private float moveSpeed;

    private bool isMoving;

    private Vector2 input;

    private float horizMove = 0f;

    private void Start()
    {
     tra    
    }


    private void Update()
    {
        HorizontalMovement();  
        if (isMoving)
        {
         transform
        }    
    }

    private void HorizontalMovement()
    {
        horizMove = input.GetAxisRaw("Horizontal") * moveSpeed;
        
        transfo
    }

    voidFixedUpdate ()
    {
        controller.Move(horizontalMove)
    }
}
