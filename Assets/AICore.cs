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
    [SerializeField] private Animator animator;
    [SerializeField] private NavMeshAgent agent;
    public Transform target;
    [SerializeField] private Health enemyHealth;
    [SerializeField] private PlayerStats playerStat;
    [SerializeField] private Transform statusTransform;

    [Header("Parameters")]
    private float distance;

    [Header("States")]
    [SerializeField] private bool isStaggered = false;



    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        playerStat = FindObjectOfType<PlayerStats>().GetComponent<PlayerStats>();

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
            }
            else
            {
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

    private void Idle()
    {
        agent.isStopped = true;
        animator.SetBool("IsIdle", true);
        animator.SetBool("IsMoving", false);
        agent.SetDestination(this.gameObject.transform.position);
    }
   

    public void AttackPlayer()
    {
        distance = Vector3.Distance(target.position, transform.position);
        if (distance <= enemyData.attackRadius && isStaggered == false)
        {
            agent.isStopped = true;
            StartCoroutine(AttackTimer());

        }
        else
        {
            agent.isStopped = false;
        }
    }

    public void Stagger()
    {
        agent.isStopped = true;
        isStaggered = true;
        StartCoroutine(StunTimer());
    }

    public IEnumerator AttackTimer()
    {
        animator.SetBool("IsMoving", false);
        animator.SetBool("IsIdle", true);
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(enemyData.timeBetweenAttack);

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
