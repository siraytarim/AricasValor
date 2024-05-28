using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerGetDamage : MonoBehaviour
    {
        public static PlayerGetDamage instance { get; private set; }

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this);
            }
            else
            {
                instance = this;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("EnemySword"))
            {
                move.instance.Playerhealth--;
                if (move.instance.Playerhealth <= 0)
                    Invoke(nameof(move.instance.DestroyPlayer), .1f);
            }
        }
    }
}
