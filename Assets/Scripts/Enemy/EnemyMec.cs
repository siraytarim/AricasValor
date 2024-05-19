using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class EnemyMec : MonoBehaviour
{
   [SerializeField] private Animator animator;
   public NavMeshAgent agent;
   public Transform player;
   public LayerMask whatIsGround, whatIsPlayer;
   public float health;

   //patroling
   public Vector3 walkPoint;
   private bool walkPointSet;
   public float walkPointRange;
   
   //attacking
   public float timeBetwwenAttacks;
   private bool alreadyAttacked;
 //  public GameObject projectile;
   
   //states
   public float sightRange, attackRange;
   public bool playerIsSightRange, playerInAttackRange;

   public GameObject enemyBullet;
   public Transform spawnPoint;
   public float enemySpeed;
   private void Awake()
   {
      player = GameObject.Find("Player").transform;
      agent = GetComponent<NavMeshAgent>();
      animator = GetComponent<Animator>();
   }

   private void Update()
   {
      animator.SetBool("isRunning",true);
      
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
      
      walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z +randomZ);
      if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround)) ;
      walkPointSet = true;
   }
   void ChasePlayer()
   {
      agent.SetDestination(player.position);
   }
   void AttackPlayer()
   {
     // animator.SetBool("isRunning", false);
      // eenmy hareekt etmeyecek
      agent.SetDestination(transform.position);
      transform.LookAt(player);
      
         //attack yapacak
         Rigidbody bRig = Instantiate(enemyBullet, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
         bRig.AddForce(transform.forward * 6f, ForceMode.Impulse);
         bRig.AddForce(transform.up * 1.2f, ForceMode.Impulse);
         Destroy(bRig, .7f);
         alreadyAttacked = true;
         Invoke(nameof(ResetAttack), timeBetwwenAttacks);
      
   }
   void ResetAttack()
   {
      alreadyAttacked = false;

   }
   public void TakeDamage(int damage)
   {
      health -= damage;
      if(health <=0)
         Invoke(nameof(DestroyEnemy),.5f);
   }
   void DestroyEnemy()
   {
      Destroy(gameObject);
   }
   void OnDrawGizmosSelected()
   {
      Gizmos.color=Color.red;
      Gizmos.DrawWireSphere(transform.position,attackRange);
      Gizmos.color = Color.yellow;
      Gizmos.DrawWireSphere(transform.position,sightRange);
   }
}
