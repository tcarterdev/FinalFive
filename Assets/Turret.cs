using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private EnemyData enemyData;
    public Transform Player;
    private float distance;
    public float howClose;
    public float bulletForce, fireRate, timeofLastShot;
    public Transform head, barrelFirepoint;
    public GameObject projectile;

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
        bullet.GetComponent<Rigidbody>().AddForce(Player.transform.position * bulletForce);
        Destroy(bullet, 3);

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(head.position, howClose);
    }
}
