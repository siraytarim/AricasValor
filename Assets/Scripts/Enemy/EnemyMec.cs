using System;
using System.Collections;
using System.Collections.Generic;
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
      [SerializeField] Transform player;
      [SerializeField] NavMeshAgent agent;
      [SerializeField] LayerMask whatIsGround, whatIsPlayer;

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
      }

      private void Update()
      {
         playerIsSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
         playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

         if (!playerIsSightRange && !playerInAttackRange) Patroling();
         if (playerIsSightRange && !playerInAttackRange) ChasePlayer();
         if (playerIsSightRange && playerInAttackRange) AttackPlayer();
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
         agent.SetDestination(transform.position);
         animator.SetBool("EnemyIsAttack", true);
          Invoke("DamageParticle",.5f);
         transform.LookAt(player);
         Debug.Log(health);
      }

      void DamageParticle()
      {
         particle = Instantiate(damageParticle, transform.position, Quaternion.identity);
         ResetAttack();

      }

      void ResetAttack()
      {
         alreadyAttacked = false;
         Invoke("DamageParticle", 20f);

      }
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
