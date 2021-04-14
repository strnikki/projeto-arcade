using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] float health = 10f;
    [SerializeField] float despawnTimer = 2f;

    private Rigidbody rb;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void TakeDamage(Vector3 dir, float damage)
    {
        health -= damage;
        
        if(health <= 0)
        {
            rb.AddForce(dir, ForceMode.Impulse);
            Destroy(this.gameObject, despawnTimer);
        }
    }
    
}
