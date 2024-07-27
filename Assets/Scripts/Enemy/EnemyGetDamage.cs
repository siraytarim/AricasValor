using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using Player.Health;
using UnityEngine;

namespace Enemy
{ 
    namespace Health
    {
        public class EnemyGetDamage : MonoBehaviour
        {
            [Header("HealthBar")] [SerializeField] private int maxhealth;
            [SerializeField] private int currentHealth;
            [SerializeField] private HealthBarr healthBar;
            [SerializeField] private int damage;

            [SerializeField] private ParticleSystem damageEffect;
            ParticleSystem damageEffectInstance;
            public float damageMultiplier = 1.0f;
            public static EnemyGetDamage instance { get; private set; }

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
                maxhealth = currentHealth;
                healthBar.MaxHealth(maxhealth);
                
            }
            private void OnTriggerEnter(Collider other)
            {
                if ((other.CompareTag("Sword")) && (move.instance.isAttacked))
                {
                    Debug.Log("player saldırdı");
                        EnemyMec.Instance.animator.SetTrigger("Hitted");
                        damageEffectInstance = Instantiate(damageEffect, transform.position, Quaternion.identity);
                        EnemyMec.Instance.health -= damageMultiplier;
                        currentHealth -= damage;
                        healthBar.SetHealht(currentHealth);
                    if (EnemyMec.Instance.health <= 0)
                    {
                            EnemyMec.Instance.animator.SetBool("isDie", true);
                            Invoke("DestroyEnemy", 1.7f);
                    }
                }
            }
            void DestroyEnemy()
            {
                Destroy(gameObject);
                damageMultiplier += 1.2f;
            }
        }
        }
    }
