using System;
using UnityEngine;
using System.Collections;

public class DashMask : Mask
{
    public float force;
    public float cooldown = 1f;

    
    private bool cooldownActive = false;
    public override void Ability(float input)
    {
        if (currentOwner.isPlayer)
        {
            currentOwner.rb.AddForce(currentOwner.cam.forward * force, ForceMode.Impulse);
        }
        else
        {
            currentOwner.rb.AddForce((PlayerSignature.Instance.transform.position - currentOwner.transform.position) * force, ForceMode.Impulse);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //print("velocity " + currentOwner.velocity);
        if (currentOwner.velocity.magnitude < 10)
        {
            return;
        }
        
        Health h = other.gameObject.GetComponent<Health>();
        if (h && !currentOwner.isPlayer && h.isPlayer)
        {
            h.TakeDamage(1);
            return;
        }
        if (h && !h.isPlayer)
        {
            h.TakeDamage(1);
        }
        print("enter trigger");
    }

    private IEnumerator cooldownRoutine()
    {
        cooldownActive = true;
        yield return new WaitForSeconds(cooldown);
        cooldownActive = false;
    }
}
