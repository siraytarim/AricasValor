using System;
using System.Collections;
using System.Collections.Generic;
using Enemy;
using UnityEngine;

namespace Player
{
    namespace Health
    {
        public class PlayerGetDamage : MonoBehaviour
        {
            [Header("HealthBar")] 
            [SerializeField] private int maxhealth ;
            [SerializeField] private int currentHealth;
            [SerializeField] public HealthBarr healthBar;
            [SerializeField] private int damage;
            private Animator anim;
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

            private void Start()
            {
                anim = GetComponent<Animator>();
                maxhealth = currentHealth;
                healthBar.MaxHealth(maxhealth);
                
            }

            void Update()
            {
                if (EnemyMec.isAttacked)
                {
                    PlayerHitted();
                }
            }

            void PlayerHitted()
            {
                anim.SetTrigger("Hitted");
                Debug.Log(currentHealth + " player health");
                currentHealth -= damage;
                healthBar.SetHealht(currentHealth);
                if (currentHealth <= 0)
                {
                    DestroyPlayer();
                }
            }
            void DestroyPlayer()
            {
                Destroy(gameObject);
            }
        }
    }
}
