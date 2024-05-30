using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEditor.PackageManager.Events;
using UnityEngine.UI;

public class Collectibles : MonoBehaviour
{
   [SerializeField] GameObject collectible;
   [SerializeField] Animator _openanimator;
   [SerializeField] Animator _coinanimator;
   //[SerializeField] GameObject coinBase;
   [SerializeField] Text coinText;
    int coinCount=0;
    private void Start()
    {
       UpdateCoins();
    }

   private void Update()
   {
      ChestOpen();
   }

   private void ChestOpen()
   {
      if (Input.GetMouseButtonDown(1))
      {
         _openanimator.SetBool("chestOpen", true); //sandığı açma
         Invoke("AddCoinText",2.5f);
         Invoke("GoldAnim", 1.6f);
      }
   }

   void AddCoinText()
   {
      coinCount += 30;
      UpdateCoins();
   }
   void GoldAnim()
   {
      _coinanimator.SetTrigger("Gold");
     Invoke("Destroy",.75f);
   }

  void Destroy()
   {
      Destroy(collectible);
   }

   void UpdateCoins()
   {
      coinText.text = "" + coinCount.ToString();
   }
}
