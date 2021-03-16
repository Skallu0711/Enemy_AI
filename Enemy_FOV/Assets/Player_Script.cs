using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Script : MonoBehaviour
{
    private float horizontalMovement;
    private float verticalMovement;

    [SerializeField] private float speed = 10f;

    private void FixedUpdate()
    {
        Move();
    }
    
    private void Move()
    {
        horizontalMovement = transform.position.x + speed * Time.deltaTime * Input.GetAxisRaw("Horizontal");
        verticalMovement = transform.position.y + speed * Time.deltaTime * Input.GetAxisRaw("Vertical");
        
        transform.position = new Vector3(horizontalMovement, verticalMovement, 0);

    }
    
}
