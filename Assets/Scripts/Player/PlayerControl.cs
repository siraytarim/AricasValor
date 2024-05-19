using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] float moveSpeed;
    [SerializeField] float turnSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] float gravityScale;
    [SerializeField] float rotateSpeed;
    public CharacterController controller;

    private Vector3 moveDirection;
    
    public Animator animator;
    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Running();
        Attack();
        
        float yStore = moveDirection.y;
        moveDirection = (transform.forward * Input.GetAxis("Vertical")) +
                        (transform.right * Input.GetAxis("Horizontal"));
        moveDirection = moveDirection.normalized * moveSpeed;
        moveDirection.y = yStore;

        if (controller.isGrounded)
        {
            moveDirection.y = 0f;
            if (Input.GetButtonDown("Jump"))
                moveDirection.y = jumpForce;

            moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale * Time.deltaTime);
            controller.Move(moveDirection * Time.deltaTime);

            animator.SetBool("isGrounded", controller.isGrounded);
            animator.SetFloat("Speed", Mathf.Abs(Input.GetAxis("Vertical")) + Mathf.Abs(Input.GetAxis("Horizontal")));
        }

    }
    public void Attack()
    {
        if (Input.GetKey(KeyCode.A))
        {
            animator.SetTrigger("isAttack");    
           
        }
    }
    public void Running()
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            animator.SetBool("isRunning",true);
           Quaternion newRotation=Quaternion.LookRotation(new Vector3(moveDirection.x,0f,moveDirection.z));
          player.transform.rotation=Quaternion.Slerp(transform.rotation,newRotation,rotateSpeed*Time.deltaTime);
        //  player.transform.Translate(Vector3.forward * Time.deltaTime);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }
}
