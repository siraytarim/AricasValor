using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CollectibleCoins : MonoBehaviour
{
    [SerializeField] private GameObject collectibles;
    [SerializeField] private Animator coinIncrement;
    [SerializeField] Text coinText;
    int coinCount=0;
    private void Start()
    {
        UpdateCoins();
    }

   /* private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RightClick();
        }
    }*/

    void UpdateCoins()
    {
        coinText.text = "" + coinCount.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RightClick();
        }
    }

    void AddCoinText()
    {
        coinCount += 40;
        UpdateCoins();
    }

    private void RightClick()
    { 
        AddCoinText();
        coinIncrement.SetTrigger("CoinIncrement");
        Destroy(gameObject);
            
       
    }
}
