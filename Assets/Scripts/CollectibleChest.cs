using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CollectibleChest : MonoBehaviour
{
    [SerializeField] private GameObject collectibles;
    [SerializeField] private Animator coinIncrement;
    [SerializeField] private Animator chestAnim;
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
            chestAnim.SetTrigger("chestOpen");
            Invoke("RightClick",1f);
        }
    }

    void AddCoinText()
    {
        coinCount += 20;
        UpdateCoins();
    }

    private void RightClick()
    { 
        AddCoinText();
        coinIncrement.SetTrigger("CoinIncrement");
        Destroy(collectibles);
            
       
    }
}
