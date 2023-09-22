using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMelee : MonoBehaviour
{

    [SerializeField] private EnemyData enemyData;
    [SerializeField] private AICore aiCore;
    [SerializeField] private Animator animator;

    public void Start()
    {
        animator = GetComponent<Animator>();
        aiCore = GetComponent<AICore>();
    }
    public void AttackTarget()
    {
        animator.SetTrigger("Attack");
        aiCore.target.GetComponent<PlayerStats>().TakeDamageToPlayer(enemyData.damage);
        
    }

   
}
