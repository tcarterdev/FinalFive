using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{

    private Transform Player;
    private float distance;
    public float howClose;
    public float bulletForce, fireRate, timeofLastShot;
    public Transform head, barrelFirepoint;
    public GameObject projectile;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(Player.position, transform.position);

        if (distance <= howClose)
        {
            head.LookAt(Player);
            if (Time.time >= timeofLastShot)
            {
                timeofLastShot = Time.time + 1f / fireRate;
                Shoot();
            }

            
        }
    }
    void Shoot()
    {
        GameObject bullet = Instantiate(projectile, barrelFirepoint.position, transform.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * bulletForce);
        Destroy(bullet, 3);

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(head.position, howClose);
    }
}
