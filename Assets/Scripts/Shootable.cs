using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shootable : MonoBehaviour
{
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void TakeDamage(Vector3 impactForce, int damage)
    {
        rb.AddForce(impactForce, ForceMode.Impulse);
    }
}
