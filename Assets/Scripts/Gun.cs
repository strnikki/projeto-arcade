using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] int weaponDamage = 10;
    [SerializeField] float weaponImpact = 100f;
    [SerializeField] float shootingCooldown = .5f;

    [SerializeField] ParticleSystem sparks;

    private bool isHitScan;
    private bool canShoot = true;
    
    private Animator gunAnimator;

    void Start()
    {
        gunAnimator = GetComponent<Animator>();
    }

    public void Shoot(Transform shootOrigin)
    {
        if(canShoot)
        {
            gunAnimator.SetTrigger("Shooting");
            AudioManager.instance.Play("TiroUnicoPolvora");
            sparks.Play();
            canShoot = false;
            StartCoroutine(ShootTimer());
            
            RaycastHit hit;

            if(Physics.Raycast(shootOrigin.transform.position, shootOrigin.transform.forward, out hit, 1000f))
            {
                Debug.Log(hit.transform.name);

                Target target = hit.transform.GetComponent<Target>();
                if(target != null)
                {
                    target.TakeDamage(transform.forward * weaponImpact, weaponDamage);
                }
            }
        }
        
    }

    IEnumerator ShootTimer()
    {
        yield return new WaitForSeconds(shootingCooldown);
        canShoot = true;
    }
}
