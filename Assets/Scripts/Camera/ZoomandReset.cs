using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomandReset : MonoBehaviour
{
   [SerializeField] private Animator _animator;

   private void Start()
   {
      Invoke("Animation", 1.5f);
   }

   void Animation()
   {
      _animator.SetBool("entryEnd", true);
   }
}
