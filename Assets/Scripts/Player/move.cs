using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    private GameObject player;
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

    void Start()
    {
        controller = GetComponent<CharacterController>();
        player = GetComponent<GameObject>();
    }

    void Update()
    {
        Attack();
        // Zemin kontrolü
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Hareket girişi
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        controller.Move(move * moveSpeed * Time.deltaTime);

        if (move != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
        }

        // Zıplama girişi
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y);
        }

        // Yerçekimi uygulaması
        velocity.y += Physics.gravity.y * gravityScale * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    public void Attack()
    {
        if (Input.GetKey(KeyCode.P))
        {
            _animator.SetTrigger("isAttack");

        }
    }

}
