using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "AI/Enemy")]
public class EnemyData : ScriptableObject
{
    [Header("Info")]
    public new string name;

    [Header("Parameters")]
    public bool isRanged;
    public bool isMelee;
    public float damage;
    public float attackRadius;
    public float timeBetweenAttack;
    public float timeBetweenAttackCD;
    public float attackSpeed;
    public float attackForce;
    public float detectionRadius;
    public float staggerTime = 2f;

    [Header("Audio")]
    public AudioClip enemyIdleFX;
    public AudioClip[] enemyDetectFX;
    public AudioClip enemyAttack;
    public AudioClip enemyDeathFX;

    [Header("Particles")]
    public ParticleSystem takeDamageVFX;
    public ParticleSystem deathVFX;
    public GameObject staggerParticle;



    void Start()
    {
        
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
