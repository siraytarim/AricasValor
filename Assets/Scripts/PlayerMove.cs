using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    
    public FixedJoystick joystick;
    public float SpeedMove = 5f;
    private CharacterController controller;
    private Animator anim;
    void Start()
    {
        controller= GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    
    void Update()
    {
        Vector3 Move = transform.right * joystick.Horizontal + transform.forward * joystick.Vertical;
        if (joystick.Horizontal != 0)
        {
          //  anim.SetBool("Hareket", true);
        }
        else
        {
           // anim.SetBool("Hareket", false);
        }
        controller.Move(Move * SpeedMove * Time.deltaTime);
    }
}
