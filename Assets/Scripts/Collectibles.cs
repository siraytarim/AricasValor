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
   //[SerializeField] Text coinText;
    int coins;
    private void Start()
   {
      //coins = Convert.ToInt32(coinText.text);
   }

   private void OnMouseDown()
   {
     
      _openanimator.SetBool("chestOpen", true);    //sandığı açma
      Invoke("GoldAnim",1.6f);
      coins += 30;
   }

   void GoldAnim()
   {
      _coinanimator.SetBool("Gold", true);
     //Invoke("Destroy",1.6f);
   }

 /*  void Destroy()
   {
      Destroy(collectible);
   }*/
}
