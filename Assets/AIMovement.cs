using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class AIMovement : MonoBehaviour
{
   [SerializeField] private EnemyData enemyData;
    public Transform target;
    [SerializeField] private Animator animator;
    [SerializeField] private NavMeshAgent agent;
    private float distance;



    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       distance = Vector3.Distance(target.position, transform.position);
        if (distance <= enemyData.detectionRadius)
        {
            agent.SetDestination(target.position);
        }
        

        animator.SetBool("IsMoving", true);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.gameObject.transform.position ,enemyData.detectionRadius);
    }
}
