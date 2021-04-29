using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    [SerializeField] float projectileImpact = 1f;

    private Rigidbody rb;

    public Vector3 target { get; set; }


    
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + transform.forward * speed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter(Collision other)
    {
        if(LayerMask.LayerToName(other.gameObject.layer) != "Ground")
        {
            other.gameObject.GetComponent<Target>().TakeDamage(transform.forward * projectileImpact, 10);
            Debug.Log(other.gameObject.name);
            Destroy(this.gameObject);
        }
        else 
        {
            Destroy(this.gameObject);
        }
    }
}
