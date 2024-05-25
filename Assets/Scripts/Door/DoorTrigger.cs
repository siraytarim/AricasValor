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
    public GameObject openPanel = null;
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
                _animator.SetBool("IsDoorOpen", true);
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
            _animator.SetBool("IsDoorOpen", false);
            openPanel.SetActive(false);
            LoadNextScene();
        }
    }
    private bool IsOpenPanelActive()
    {
            return openPanel.activeInHierarchy;
    }
    
    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        SceneManager.LoadScene(nextSceneIndex);
    }

}


