using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject enemy;
    [SerializeField] private float rotateSpeed;
    
    private Vector3 moveDirection;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            animator.SetBool("isRunning",true);
            Quaternion newRotation=Quaternion.LookRotation(new Vector3(moveDirection.x,0f,moveDirection.z));
            enemy.transform.rotation=Quaternion.Slerp(transform.rotation,newRotation,rotateSpeed*Time.deltaTime);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }
}
