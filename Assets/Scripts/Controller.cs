using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

[RequireComponent(typeof(CharacterController))]

public class Controller : MonoBehaviour
{
    public Rigidbody rb;
    public float moveSpeed;
    
    private Vector3 moveDirection;

    public InputActionReference move;

    private void Update()
    {
        moveDirection = move.action.ReadValue<Vector3>();
    }


    private void FixedUpdate()
    {
        rb.velocity = new Vector3(moveDirection.x * moveSpeed, 0f,
            moveDirection.z * moveSpeed);
    }
}
