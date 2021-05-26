using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float minDistance;
    [SerializeField] float speed;
    [SerializeField] float shootingCooldown = 1f;
    [SerializeField] float health = 10;

    [SerializeField] Transform projectile;
    [SerializeField] Transform gun;
    [SerializeField] Transform gunBarrel;

    [SerializeField] ParticleSystem blood;

    private GameManager gameManager;
    private Transform player;
    private Rigidbody rb;
    
    private bool canShoot = true;
    private bool alive = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player").transform;
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(alive)
        {
            EnemyLook();
        }
    }

    void FixedUpdate()
    {
        if(alive)
        {
            EnemyMove();
        }
    }

    private void EnemyLook()
    {
        Vector3 target = new Vector3(player.position.x, transform.position.y, player.position.z);
        transform.LookAt(target);
        gun.transform.LookAt(player.position);
    }

    private void EnemyMove()
    {
        Vector3 enemyHorizPos = new Vector3(transform.position.x, 0, transform.position.z);
        Vector3 playerHorizPos = new Vector3(player.position.x, 0, player.position.z);

        //Refactor to use nav mesh
        Vector3 direction = (player.position - transform.position).normalized;
        
        if(Vector3.Distance(enemyHorizPos, playerHorizPos) > minDistance)
        {
            rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
        }
        else 
        {
            if(canShoot)
            {
                Shoot();
            }
        }
    }

    private void Shoot()
    {
        Quaternion projRotation = Quaternion.Euler(gun.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, projectile.rotation.eulerAngles.z);
        Instantiate(projectile, gunBarrel.position + gun.forward, projRotation);   
        
        canShoot = false;
        StartCoroutine(ShootTimer());

    }

    IEnumerator ShootTimer()
    {
        yield return new WaitForSeconds(shootingCooldown);
        canShoot = true;
    }

    public void TakeDamage(Vector3 impactForce, int damage)
    {
        health -= damage;
        blood.Play();
        //rb.AddForce(impactForce, ForceMode.Impulse);

        if(health <= 0)
        {
            Die(impactForce);
        }
    }

    private void Die(Vector3 impactForce)
    {
        rb.constraints = RigidbodyConstraints.None;
        rb.AddForce(impactForce, ForceMode.Impulse);
        alive = false;

        gameManager.UpdateScore(1);

        Destroy(this.gameObject, 1f);
    }
}
