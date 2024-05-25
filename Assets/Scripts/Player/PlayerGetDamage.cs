using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGetDamage : MonoBehaviour
{
    public static move instance { get; private set; }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            instance.health--;
            if(instance.health <=0)
                Invoke(nameof(instance.DestroyPlayer),.1f);
        }
    }
}
