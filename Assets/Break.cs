using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Break : MonoBehaviour, IDamageable
{
    public float health = 50;
    public GameObject fractured;
    public float breakForce;

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Fracture();
        }
    }

    public void Fracture()
    {
        GameObject fracture = Instantiate(fractured, transform.position, transform.rotation);
        
        foreach (Rigidbody rb in fracture.GetComponentsInChildren<Rigidbody>())
        {
            Vector3 force = (rb.transform.position - transform.position).normalized * breakForce;
                rb.AddForce(force, ForceMode.Impulse);
        }
        Destroy(gameObject);
    }

    


}
