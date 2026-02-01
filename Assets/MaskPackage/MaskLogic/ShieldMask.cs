using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class ShieldMask : Mask
{
    public Transform shield;
    public float shieldSpeed = 10f;
    public float bounce = 50f;

    private Collider shieldCollider;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        shieldCollider = shield.GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        print("enter trigger");

        if (other.gameObject.layer == LayerMask.NameToLayer("Bullet"))
        {
            GameObject.Destroy(other.gameObject);
            return;
        }
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            //shield launches player
            other.gameObject.GetComponent<Rigidbody>().AddForce((shield.forward + Vector3.up) * bounce);  
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            //shield stuns agent temporarily.
            //other.gameObject.GetComponent<NavMeshAgent>().isStopped = true;
            if(!active)
                StartCoroutine(StopAgent(other.gameObject.GetComponent<NavMeshAgent>()));
        }
    }
    private void OnTriggerExit(Collider other)
    {
        print("exit trigger");

        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            //other.gameObject.GetComponent<NavMeshAgent>().isStopped = false;
        }
    }
    
    bool active = false;
    private IEnumerator StopAgent(NavMeshAgent agent)
    {
        active = true;
        agent.isStopped = true;
        yield return new WaitForSeconds(1.5f);
        active = false;
        agent.isStopped = false;
    }
}
