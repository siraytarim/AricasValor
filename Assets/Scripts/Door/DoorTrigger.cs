using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class DoorTrigger : MonoBehaviour
{
    private Animator _animator;
    [SerializeField] private Animator camAnim;
    public GameObject openPanel = null;
    private void Start()
    {
        _animator = GetComponent<Animator>();
        }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            camAnim.SetBool("ısTriggered", true);
            openPanel.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _animator.SetBool("IsDoorOpen", false);
            openPanel.SetActive(false);
            camAnim.SetBool("ısTriggered", false);
            SceneManager.LoadScene(1);
        }
    }

    private bool IsOpenPanelActive()
    {
            return openPanel.activeInHierarchy;
    }
    private void Update()
    {
        if (IsOpenPanelActive())
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                openPanel.SetActive(true);
                _animator.SetBool("IsDoorOpen", true);
            }
        }
    }
}


