using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditorInternal;

public class Gun : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GunData gunData;
    [SerializeField] private Transform gunFirepoint;
    [SerializeField] private TrailRenderer bulletTrail;

    private static Gun gun;

    [Header("Animations")]
    [SerializeField] private Animator gunAnimator;


    [Header("Gun Audio and UI")]
    [SerializeField] private AudioSource gunAudioSource;
    [SerializeField] private AudioClip hitmarkerFX;
    [SerializeField] private TMP_Text ammoCountUI;
    [SerializeField] private TMP_Text gunNameUI;
    [SerializeField] private Image hitmarkerUI;
    [HideInInspector] private float hitmarkerTimer = 0.5f;
    private Color CLEARWHITE = new Color(1, 1, 1, 0);

    [Header("Gun VFX")]
    [SerializeField] private GameObject enemyHitVFX;

    float timeSinceLastShot;

    public void Start()
    {
        //Set reloading as false on start and set current ammmo to magazine size

        gunData.reloading = false;
        gunData.currentAmmo = gunData.magSize;


        //Set Hitmarker to Invisible
        hitmarkerUI.color = CLEARWHITE;

        //Assign Player Actions
        PlayerShoot.shootInput += Shoot;

        PlayerShoot.reloadInput += StartReload;

        PlayerShoot.flashlightInput += ToggleFlash;
    }

    public void Awake()
    {
    }


    private void OnDisable()
    {
        gunData.reloading = false;
        gunAnimator.keepAnimatorControllerStateOnDisable = true;
        
        gunAnimator.SetBool("isReloading", false);
        gunAnimator.SetTrigger("FireEnd");
    }

    private void OnEnable()
    {

        
        gunAudioSource.PlayOneShot(gunData.gunReadyFX, 0.5f);
        gunAnimator.enabled = true;

        //Display Current Weapon Name
        gunNameUI.SetText(gunData.name);
    }

    public void StartReload()
    {
        if (!gunData.reloading && this.gameObject.activeSelf)
        {
            StartCoroutine(Reload());
        }
    }

    public void ToggleFlash()
    {
        
    }

    private IEnumerator Reload()
    {
        gunData.reloading = true;
        gunAnimator.SetBool("isReloading", true);
        gunAudioSource.PlayOneShot(gunData.gunReloadFX, 0.5f);
        yield return new WaitForSeconds(gunData.reloadTime);

        gunData.currentAmmo = gunData.magSize;

        gunData.reloading = false;
        gunAnimator.SetBool("isReloading", false);
    }
    private void StunEnemy()
    {
        
    }

    private IEnumerator SpawnTrail(TrailRenderer trail, RaycastHit Hit)
    {
        float time = 0;
        Vector3 startposition = trail.transform.position;

        while (time < 1)
        {
            trail.transform.position = Vector3.Lerp(startposition, Hit.point, time);
            time += Time.deltaTime / trail.time;

            yield return null;

            //MAKE SURE THESE TRAILS GET DESTROYED!
        }

        trail.transform.position = Hit.point;
    }
    private bool CanShoot() => !gunData.reloading && timeSinceLastShot > 1f / (gunData.fireRate / 60f);

    private void Shoot()
    {
        if (gunData.isHitScan && gunData.isProjectile == false)
        {
            if (gunData.currentAmmo > 0)
            {
                if (CanShoot())
                {
                    gunAnimator.SetTrigger("Fire");

                    //Muzzle Flash
                    GameObject muzzleflash = Instantiate(gunData.muzzleFlash, gunFirepoint, worldPositionStays: false);
                    Destroy(muzzleflash, 0.1f);

                    TrailRenderer trail = Instantiate(bulletTrail, gunFirepoint.position, Quaternion.identity);
                    Destroy(trail, 1f);

                    gunAudioSource.pitch = Random.Range(0.8f, 1f);
                    gunAudioSource.PlayOneShot(gunData.gunShotFX, 0.5f);


                    //Normal Shot
                    if (Physics.Raycast(gunFirepoint.position, gunFirepoint.forward, out RaycastHit hitInfo, gunData.maxDistance))
                    {
                        //Spawn Trail
                        StartCoroutine(SpawnTrail(trail, hitInfo));
                        //take damage
                        IDamageable damageable = hitInfo.transform.GetComponent<IDamageable>();
                        damageable?.TakeDamage(gunData.damage);

                        //If Hit an Enemy - Provide HitMarker Feedback
                        if (hitInfo.collider.gameObject.CompareTag("Enemy"))
                        {
                            hitmarkerUI.color = Color.white;
                            gunAudioSource.PlayOneShot(hitmarkerFX, 1f);
                            hitmarkerTimer = 0.5f;

                            GameObject bloodSpray = Instantiate(enemyHitVFX, hitInfo.point, Quaternion.identity);
                            Destroy(bloodSpray, 0.2f);
                        }
                        //Stagger shot
                        // Stagger shot
                        if (this.gameObject.activeInHierarchy && this.gunData.hasStagger == true)
                        {
                            AICore getStaggered = hitInfo.collider.gameObject.GetComponent<AICore>();
                            if (getStaggered != null)
                            {
                                getStaggered.Stagger();
                            }

                            Rigidbody enemyRB = hitInfo.rigidbody;
                            if (enemyRB != null)
                            {
                                enemyRB.AddForce(Vector3.back * this.gunData.bulletForce);
                            }
                        }




                    }

                    gunData.currentAmmo--;
                    timeSinceLastShot = 0;
                    OnGunShot();
                    gunAnimator.SetTrigger("FireEnd");
                    gunAnimator.SetTrigger("Idle");

                }
            }
        }

        if (gunData.isProjectile == true && !gunData.isHitScan)
        {
            if (gunData.currentAmmo > 0)
            {
                if (CanShoot())
                {
                    gunAnimator.SetTrigger("Fire");

                    //Muzzle Flash
                    GameObject muzzleflash = Instantiate(gunData.muzzleFlash, gunFirepoint, worldPositionStays: false);
                    Destroy(muzzleflash, 0.1f);
                    TrailRenderer trail = Instantiate(bulletTrail, gunFirepoint.position, Quaternion.identity);
                    Destroy(trail, 1f);
                    gunAudioSource.pitch = Random.Range(0.8f, 1f);
                    gunAudioSource.PlayOneShot(gunData.gunShotFX, 0.5f);

                    FireProjectile();

                    gunData.currentAmmo--;
                    timeSinceLastShot = 0;
                    OnGunShot();
                    gunAnimator.SetTrigger("FireEnd");
                    gunAnimator.SetTrigger("Idle");


                }


            }
            }
        

    }

    private void FireProjectile()
    {
        GameObject projectile = Instantiate(gunData.projectile, gunFirepoint.position, Quaternion.identity);
        projectile.GetComponent<Rigidbody>().AddForce(transform.forward * gunData.bulletForce);
        }

    private void Update()
    {

        ammoCountUI.SetText(gunData.currentAmmo + " / " + gunData.magSize);
        timeSinceLastShot += Time.deltaTime;

        Debug.DrawRay(gunFirepoint.position, gunFirepoint.forward);

        if (hitmarkerTimer > 0)
        {
            hitmarkerTimer -= Time.deltaTime;
        }
        else
        {
            hitmarkerUI.color = Color.Lerp(hitmarkerUI.color, CLEARWHITE, Time.deltaTime * 5f);
        }
    }

    
     
    private void OnGunShot()
    {
        
    }
}
