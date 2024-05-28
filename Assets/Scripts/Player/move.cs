using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace  Player{
public class move : MonoBehaviour
{
    public static move instance { get; private set; }
    
    [Header("Core")] [SerializeField] float moveSpeed;
    [SerializeField] float jumpHeight;
    [SerializeField] float turnSpeed;
    public int Playerhealth;

    [Header("Ground")] [SerializeField] Transform groundCheck;
    [SerializeField] float groundDistance;
    [SerializeField] float gravityScale;
    [SerializeField] LayerMask groundMask;
    private bool isGrounded;

    [Header("Attack")] [SerializeField] private ParticleSystem hitEffect;
    [SerializeField] Transform sword;
    [SerializeField] private Transform enemy;
    ParticleSystem hitEffectInstance; 

    private CharacterController controller;
    private GameObject player;
    private Animator _animator;

    private Vector3 velocity;
    private Vector3 moveDirection;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }
    void Start()
    {
        controller = GetComponent<CharacterController>();
        player = GetComponent<GameObject>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        _animator.SetBool("isRunning", false);
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
            _animator.SetBool("isRunning", true);
            Quaternion targetRotation = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
        }

        //attack kontrol
        Attack();

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
        if (Input.GetMouseButtonDown(0))
        {
            _animator.SetTrigger("Attack");
            Invoke("DamageParticle", .5f);
        }
    }

    void DamageParticle()
    {
        hitEffectInstance = Instantiate(hitEffect, sword.position, Quaternion.identity);

    }

    void nextAttack()
    {
        _animator.SetBool("isAttack", false);
    }

    public void DestroyPlayer()
    {
        Destroy(player);
    }

}

}
