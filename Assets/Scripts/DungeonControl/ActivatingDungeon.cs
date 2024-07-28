using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatingDungeon : MonoBehaviour
{
    [SerializeField] private GameObject[] dungeons;
    public int currentDungeonIndex=0;

    private void Start()
    {
        ActivateDungeon(currentDungeonIndex);
    }
    public void ActivateDungeon(int dungeonIndex)
    {
        for (int i = 0; i < dungeons.Length; i++)
        {
            if (i == dungeonIndex)
            {
                SetDungeonComponents(dungeons[i], true);
            }
            else
            {
                SetDungeonComponents(dungeons[i], false);
            }
        }
        currentDungeonIndex = dungeonIndex;
    }
    
    private void SetDungeonComponents(GameObject dungeon, bool isActive)
    {
        Collider[] colliders = dungeon.GetComponentsInChildren<Collider>();
        MonoBehaviour[] scripts = dungeon.GetComponentsInChildren<MonoBehaviour>();

        foreach (Collider col in colliders)
        {
            col.enabled = isActive;
        }

        foreach (MonoBehaviour script in scripts)
        {
            script.enabled = isActive;
        }
    }
    public void NextDungeon()
    {
        if (currentDungeonIndex < dungeons.Length - 1)
        {
            ActivateDungeon(currentDungeonIndex + 1);
        }
        else Debug.Log("artamıyor");
    }

    public void PreviousDungeon()
    {
        if (currentDungeonIndex > 0)
        {
            ActivateDungeon(currentDungeonIndex - 1);
        }
    }

}
