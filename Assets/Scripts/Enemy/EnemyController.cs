using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private GameObject enemy;
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpHeight;
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundDistance;
    [SerializeField] float turnSpeed;
    [SerializeField] LayerMask groundMask;
    [SerializeField] Animator _animator;
    [SerializeField] float gravityScale;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    private Vector3 moveDirection;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        enemy = GetComponent<GameObject>();
    }

    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            _animator.SetBool("isRunning",true);
            Quaternion newRotation=Quaternion.LookRotation(new Vector3(moveDirection.x,0f,moveDirection.z));
            enemy.transform.rotation=Quaternion.Slerp(transform.rotation,newRotation,turnSpeed*Time.deltaTime);
        }
        else
        {
            _animator.SetBool("isRunning", false);
        }
    }
}
