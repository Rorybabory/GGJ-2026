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

    private IEnumerator cooldownRoutine()
    {
        cooldownActive = true;
        yield return new WaitForSeconds(cooldown);
        cooldownActive = false;
    }
}
