using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyGetDamage : MonoBehaviour
    {
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
       private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Sword"))
            {
                EnemyMec.Instance.animator.SetTrigger("Hitted");
                damageEffectInstance = Instantiate(damageEffect, transform.position, Quaternion.identity);
                 EnemyMec.Instance.health -= EnemyMec.Instance.health*damageMultiplier;
                 Debug.Log(EnemyMec.Instance.health);
                 if (EnemyMec.Instance.health <= 0)
                 {
                     EnemyMec.Instance.animator.SetBool("isDie", true);
                     Invoke("DestroyEnemy", 3f);
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
