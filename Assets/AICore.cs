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

    [Header("Parameters")]
    private float distance;




    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        playerStat = FindObjectOfType<PlayerStats>().GetComponent<PlayerStats>();
    }

    private void Start()
    {



    }

    void Update()
    {        

        if (agent.isStopped == true)
        {
            animator.SetBool("IsIdle", true);
            animator.SetBool("IsMoving", false);
        }

        MoveToPlayer();
    }

    private void MoveToPlayer()
    {
        distance = Vector3.Distance(target.position, transform.position);

        //if player is within detection raduius move to player.
        if (distance <= enemyData.detectionRadius)
        {
            this.gameObject.transform.LookAt(target);
            agent.SetDestination(target.position);
            animator.SetBool("IsMoving", true);
            animator.SetBool("IsIdle", false);
            agent.isStopped = false;
        }
        else
        {
            agent.isStopped = true;
        }
        
    }

   

    public void AttackPlayer()
    {
        distance = Vector3.Distance(target.position, transform.position);
        if (distance <= enemyData.attackRadius)
        {
            agent.isStopped = true;
            StartCoroutine(AttackTimer());

        }
        else
        {
            agent.isStopped = false;
        }
    }
    public IEnumerator AttackTimer()
    {
        animator.SetBool("IsMoving", false);
        animator.SetBool("IsIdle", true);
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(enemyData.timeBetweenAttack);

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
