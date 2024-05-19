using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEditor.PackageManager.Events;

public class Collectibles : MonoBehaviour
{
   [SerializeField] GameObject collectible;
   [SerializeField] Animator _openanimator;
   [SerializeField] Animator _coinanimator;
    private void Start()
   {
      collectible.SetActive(false);
   }

   private void OnMouseDown()
   {
      collectible.SetActive(true);
      _openanimator.SetBool("chestOpen", true);
      Invoke("GoldAnim",1.6f);
   }

   void GoldAnim()
   {
      _coinanimator.SetBool("Gold", true);
   }
}
