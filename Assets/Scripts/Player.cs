using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] HealthBar healthBar;
    [SerializeField] GameObject gun;

    private PlayerMovement playerMovement;
    private PlayerLook playerLook;
    private GameManager gameManager;
    private Rigidbody rb;
    
    public int health = 100;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerLook = GetComponentInChildren<PlayerLook>();
        playerMovement = GetComponentInChildren<PlayerMovement>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    public void TakeDamage(Vector3 impactForce, int damage)
    {
        health -= damage;

        if(health <= 0)
        {
            // Avoid negative health
            health = 0;
            
            Die();
        }

        healthBar.SetHealth(health);
    }

    private void Die()
    {
        gun.SetActive(false);
        StopScripts();
        gameManager.ShowGameOverScreen();
    }

    public void StopScripts()
    {
        playerLook.enabled = false;
        playerMovement.enabled = false;
    }

    public void StartScripts()
    {
        playerLook.enabled = true;
        playerMovement.enabled = true;
    }
}
