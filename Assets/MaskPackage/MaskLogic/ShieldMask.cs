using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class ShieldMask : Mask
{
    public Transform shield;
    public float shieldSpeed = 10f;
    public float bounce = 50f;
    public float force;
    public float cooldown = 1f;
    public Collider shieldBashHitbox;
    public Transform playerTransform;
    private bool cooldownActive = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public override void Ability(float input)
    {
        if (cooldownActive) return;
        if (currentOwner.isPlayer)
        {
            currentOwner.rb.AddForce(currentOwner.cam.forward * force, ForceMode.Impulse);
        }
        else
        {
            currentOwner.rb.AddForce((playerTransform.position - currentOwner.transform.position) * force, ForceMode.Impulse);
        }

        StartCoroutine(cooldownRoutine());
    }
    
    private IEnumerator cooldownRoutine()
    {
        //float original = playerTransform.gameObject.GetComponent<Rigidbody>().linearDamping;
        //playerTransform.gameObject.GetComponent<Rigidbody>().linearDamping = 25.0f;
        cooldownActive = true;
        shieldBashHitbox.enabled = true;
        yield return new WaitForSeconds(cooldown);
        shieldBashHitbox.enabled = false;
        cooldownActive = false;
        //playerTransform.gameObject.GetComponent<Rigidbody>().linearDamping = original;
    }
    
    
    
    /*private void OnTriggerEnter(Collider other)
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
    }*/
}
