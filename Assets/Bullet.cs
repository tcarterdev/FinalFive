using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float bulletDamage;
    public PlayerStats pstats;

    public void Start()
    {
        pstats = FindObjectOfType<PlayerStats>();
    }
    public void TakeDamage()
    {
        bulletDamage -= pstats.currentHealth;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            TakeDamage();
            Destroy(gameObject);
        }

    }


}
