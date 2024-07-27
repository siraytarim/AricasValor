using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class Door2Trigger : MonoBehaviour
{
    private Animator _animator;
    [SerializeField] GameObject openPanel;
    
    private void Start()
    {
        _animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (IsOpenPanelActive())
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                openPanel.SetActive(true);
                _animator.SetBool("ısDoor2Open", true);
            }
        }
    }
   private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            openPanel.SetActive(true);
            
        }
    }
     private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DoorClose();
            openPanel.SetActive(false); 
        }
    }
    void DoorClose()
    {
        _animator.SetBool("isDoor2Open", false);
    }
    private bool IsOpenPanelActive()
    {
        return openPanel.activeInHierarchy;
    }
}


