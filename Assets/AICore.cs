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



    }

    void Update()
    {        

        

        MoveToPlayer();
    }

    private void MoveToPlayer()
    {
        distance = Vector3.Distance(target.position, transform.position);

        //if player is within detection raduius move to player.
        if (distance <= enemyData.detectionRadius && isStaggered == false)
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
            animator.SetBool("IsIdle", true);
        }
        
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
        isStaggered = true;
        agent.isStopped = true;
        animator.SetTrigger("Stagger");
        GameObject stagger = Instantiate(enemyData.staggerParticle, statusTransform.position, Quaternion.identity);
        Instantiate(stagger, this.statusTransform, worldPositionStays: false);
        Destroy(stagger, 2f);
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
