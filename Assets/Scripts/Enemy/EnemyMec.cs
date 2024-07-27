using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

namespace Enemy
{
   public class EnemyMec : MonoBehaviour
   {
      public static EnemyMec Instance { get; private set; }

      [Header("Core")] [SerializeField] float enemySpeed;
      [SerializeField] public static bool isAttacked;
      [SerializeField] Transform player;
      [SerializeField] NavMeshAgent agent;
      [SerializeField] LayerMask whatIsGround, whatIsPlayer;
      [SerializeField] private Transform Enemy;

      public float health = 5f;

      public Animator animator;
      [SerializeField] private ParticleSystem damageParticle;
      ParticleSystem particle;

      [Header("Patroling")] public Vector3 walkPoint;
      private bool walkPointSet;
      public float walkPointRange;

      [Header("Attacking")] private bool alreadyAttacked;
      //  public GameObject projectile;

      [Header("States")] public float sightRange, attackRange;
      public bool playerIsSightRange, playerInAttackRange;
      float lastAttackTime = 0f;  // Son saldırı zamanını saklar
      float attackInterval = 20f;  

      private void Awake()
      {  if (Instance != null && Instance != this)
            {
               Destroy(this);
            }
            else
            {
               Instance = this;
            }
            
         agent = GetComponent<NavMeshAgent>();
         animator = GetComponent<Animator>();
         isAttacked = false;
      }

      private void Update()
      {
         playerIsSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
         playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

         if (!playerIsSightRange && !playerInAttackRange) Patroling();
         if (playerIsSightRange && !playerInAttackRange) ChasePlayer();
         if (playerIsSightRange && playerInAttackRange && (Time.time - lastAttackTime >= attackInterval) && !alreadyAttacked)
         {
            AttackPlayer();
         }
      }
   

      void Patroling()
      {
         if (!walkPointSet) SearchWalkPoint();

         if (walkPointSet)
            agent.SetDestination(walkPoint);

         Vector3 ditancetoWalkPoint = transform.position - walkPoint;

         if (ditancetoWalkPoint.magnitude < 1f)
            walkPointSet = false;
      }

      void SearchWalkPoint()
      {
         //random nokta hesaplama
         float randomZ = UnityEngine.Random.Range(-walkPointRange, walkPointRange);
         float randomX = UnityEngine.Random.Range(-walkPointRange, walkPointRange);

         walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
         animator.SetBool("EnemyIsRunning", true);
         if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround)) ;
         walkPointSet = true;
      }

      void ChasePlayer()
      {
         agent.SetDestination(player.position);
      }

      void AttackPlayer()
      {
         // eenmy hareekt etmeyecek
        // agent.SetDestination(Enemy.position);
        agent.SetDestination(player.position);
         isAttacked = true;
         animator.SetTrigger("isEnemyAttacked");
         particle = Instantiate(damageParticle, transform.position, Quaternion.identity);
         transform.LookAt(player);
         Debug.Log(health + " player health");
         ResetAttack();
      }
      void ResetAttack()
      {
         alreadyAttacked = false;
         Invoke("AttackPlayer", 20f);

      }
      
    /*  IEnumerator AttackRoutine()
      {
         yield return new WaitForSeconds(0.5f);  // Particle system'in gecikme süresi
         DamageParticle();
      }*/

   /*   void DamageParticle()
      {
         particle = Instantiate(damageParticle, transform.position, Quaternion.identity);
         ResetAttack();

      }*/

      
      public void DestroyEnemy()
      {
         Destroy(gameObject);
      }

      void OnDrawGizmosSelected()
      {
         Gizmos.color = Color.red;
         Gizmos.DrawWireSphere(transform.position, attackRange);
         Gizmos.color = Color.yellow;
         Gizmos.DrawWireSphere(transform.position, sightRange);
      }
   }
}
