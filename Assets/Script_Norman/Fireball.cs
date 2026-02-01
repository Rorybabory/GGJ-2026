using System;
using System.Collections;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public Rigidbody rb;
    public Vector3 spawnForce;
    public float gravity;
    public float dmg = 1f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb.AddForce(transform.TransformVector(spawnForce), ForceMode.Impulse);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.linearVelocity -= new Vector3(0, gravity * Time.deltaTime, 0);
    }

    public void OnTriggerEnter(Collider other)
    {
        print("enter fireball collision");
        
        Health h =  other.gameObject.GetComponent<Health>();
        if (h != null)
        {
            h.TakeDamage(dmg);
        }
        GameObject.Destroy(this.gameObject);
    }
}
