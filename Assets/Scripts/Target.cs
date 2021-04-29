using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody rb;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void TakeDamage(Vector3 impactForce, int damage)
    {
        Debug.Log("entrou");
        switch (this.tag)
        {
            case "Player":
                this.GetComponent<Player>().TakeDamage(impactForce, damage);
                break;
            case "Enemy":
                this.GetComponent<Enemy>().TakeDamage(impactForce, damage);
                break;
            case "Shootable":
                this.GetComponent<Shootable>().TakeDamage(impactForce, damage);
                break;
        }
    

    }
    
}
