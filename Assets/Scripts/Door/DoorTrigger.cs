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
    [SerializeField] GameObject openPanel;
    [SerializeField] ParticleSystem stoneParticle;
    ParticleSystem spawnPoint;
    [SerializeField] private GameObject StoneStack;

    [SerializeField] Transform stones;
    
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
            Invoke("ParticleStart",1f);
        }
    }

    void ParticleStart()
    {
        spawnPoint = Instantiate(stoneParticle, stones.position, Quaternion.identity);
        Invoke("DestroyStoneStack", .4f);;

    }
    void DestroyStoneStack()
    {
        Destroy(StoneStack);
    }

    private bool IsOpenPanelActive()
    {
        return openPanel.activeInHierarchy;
    }
}


