using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonTransition : MonoBehaviour
{
    public ActivatingDungeon activatingdungeon;
    public bool isNext=false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNext = true;
          //  Invoke("IsNextFalse",0f);
            if (isNext)
            {
                activatingdungeon.NextDungeon();
                Debug.Log(activatingdungeon.currentDungeonIndex);
            }
            else
            {
                activatingdungeon.PreviousDungeon();
            }
        }
    }

   /* void IsNextFalse()
    {
        isNext = false;
    }*/
}
