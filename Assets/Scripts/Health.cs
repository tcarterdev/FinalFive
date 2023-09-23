using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    public float health = 100;
    public AICore aiCore;

    public void Start()
    {
        if (this.gameObject.CompareTag("Enemy"))
        {
            aiCore = GetComponent<AICore>();
        }
        
    }
    public void TakeDamage(float damage)
    {

        health -= damage;
        if (health <= 0 && this.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }

        if (health <= 0 && this.gameObject.CompareTag("Enemy"))
        {
            StartCoroutine(aiCore.Death());
        }
        
    }





}
