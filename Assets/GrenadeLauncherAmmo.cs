using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeLauncherAmmo : MonoBehaviour
{
    public GunData gunData;
    public float explosionRadius;
    public float explosionForce;
    public GameObject explosionPrefab;

    public void OnCollisionEnter(Collision collision)
    {
        Explode();
        Destroy(gameObject);
    }

    void Explode()
    {
        // Create the explosion effect
       GameObject explosionEffect = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(explosionEffect, 1f);
        // Find nearby colliders in the explosion radius
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (var hitCollider in hitColliders)
        {
            Health health = hitCollider.GetComponent<Health>();
            if (health != null)
            {
                // Apply damage to objects with health component
                health.TakeDamage(gunData.damage);
            }

            Rigidbody rb = hitCollider.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Apply force to rigidbodies in the explosion radius
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(this.gameObject.transform.position, explosionRadius);
    }
}
