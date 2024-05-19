using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    [SerializeField] private ParticleSystem ps;

    [SerializeField]
    private Transform transform;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            Instantiate(ps, transform.position, ps.transform.rotation);
        
    }
    
}
