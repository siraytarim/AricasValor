using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonTransition : MonoBehaviour
{
     ActivatingDungeon activatingdungeon;
    public bool isNext;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (isNext)
            {
                activatingdungeon.NextDungeon();
            }
            else
            {
                activatingdungeon.PreviousDungeon();
            }
        }
    }
}
