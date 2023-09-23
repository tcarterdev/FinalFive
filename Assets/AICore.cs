using Newtonsoft.Json.Bson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class AICore : MonoBehaviour
{

    //The AI Core

    [Header("References")]
    [SerializeField] private EnemyData enemyData;
    [SerializeField] private AIMelee aiMelee;
    [SerializeField] private Animator animator;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private AudioSource enemyAudioSource;
    [SerializeField] private bool hasPlayedAudio;


    public Transform target;
    [SerializeField] private Health enemyHealth;
    [SerializeField] private PlayerStats playerStat;
    [SerializeField] private Transform statusTransform;

    [Header("Parameters")]
    private float distance;
    private float attackRadius;

    private float initialDetectionRadius;

    [Header("States")]
    [SerializeField] private bool isStaggered = false;



    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        playerStat = FindObjectOfType<PlayerStats>().GetComponent<PlayerStats>();
        aiMelee = GetComponent<AIMelee>();
        enemyAudioSource = GetComponent<AudioSource>();

      
            enemyData.timeBetweenAttack = 3;
            enemyData.timeBetweenAttackCD = 3;

        initialDetectionRadius = enemyData.detectionRadius;
    }

    private void Start()
    {
        Idle();
        

    }
    void Update()
    {
        distance = Vector3.Distance(target.position, transform.position);
        if (!isStaggered)
        {
            if (distance <= enemyData.detectionRadius)
            {
                MoveToPlayer();

                if (!hasPlayedAudio)
                {
                    enemyAudioSource.PlayOneShot(enemyData.enemyDetectFX[UnityEngine.Random.Range(0, enemyData.enemyDetectFX.Length)]);
                    hasPlayedAudio = true;
                }

                if (enemyData.timeBetweenAttack > 0 && isStaggered == false)
                {
                    enemyData.timeBetweenAttack -= Time.deltaTime;
                }
                else
                {
                    enemyData.timeBetweenAttack = enemyData.timeBetweenAttackCD;
                    aiMelee.AttackTarget();
                    animator.SetBool("IsIdle", true);

                }
            }
            else
            {
                hasPlayedAudio = false;
                Idle();
            }
        }
        else
        {
            // Stop the agent when staggered
            agent.SetDestination(transform.position);
            Idle();
        }
    }

    private void MoveToPlayer()
    {
            agent.isStopped = false;
            this.gameObject.transform.LookAt(target);
            agent.SetDestination(target.position);
            animator.SetBool("IsMoving", true);
            animator.SetBool("IsIdle", false);
    }

    private void AggroSFX()
    {

    }

    private void Idle()
    {
        agent.isStopped = true;
        animator.SetBool("IsIdle", true);
        animator.SetBool("IsMoving", false);
        agent.SetDestination(this.gameObject.transform.position);
    }
   

  

    public void Stagger()
    {
        agent.isStopped = true;
        isStaggered = true;
        StartCoroutine(StunTimer());
    }


    public IEnumerator StunTimer()
    {

        animator.SetTrigger("Stagger");
        GameObject stagger = Instantiate(enemyData.staggerParticle, statusTransform.position, Quaternion.identity);
        Instantiate(stagger, this.statusTransform, worldPositionStays: false);
        Destroy(stagger, 2f);
        yield return new WaitForSeconds(enemyData.staggerTime);
        isStaggered = false;
        Idle();

    }

    public IEnumerator Death()
    {
        animator.SetTrigger("Death");
        agent.isStopped = true;

        // Set the local detection radius to 0.01 for the current game object
        enemyData.detectionRadius = 0.01f;

        CapsuleCollider ccolider = this.gameObject.GetComponent<CapsuleCollider>();
        ccolider.enabled = false;

        enemyAudioSource.PlayOneShot(enemyData.enemyDeathFX, 0.5f);
        yield return new WaitForSeconds(3);
        // Reset the detection radius to its initial value
        enemyData.detectionRadius = initialDetectionRadius;
        Destroy(this.gameObject);
    }

    private void OnDrawGizmos()
    {
        //Debug: Detection Radius
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(this.gameObject.transform.position ,enemyData.detectionRadius);

        //Debug: Attack Radius
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.gameObject.transform.position, enemyData.attackRadius);
    }
}
