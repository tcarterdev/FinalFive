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
    public float attackSpeed;
    public float attackForce;
    public float detectionRadius;

    [Header("Audio")]
    public AudioClip enemyIdleFX;
    public AudioClip enemyAttack;
    public AudioClip enemyDeathFX;

    [Header("Particles")]
    public ParticleSystem takeDamageVFX;
    public ParticleSystem deathVFX;




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
