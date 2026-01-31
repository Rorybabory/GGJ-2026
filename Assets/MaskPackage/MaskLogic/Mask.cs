using System;
using UnityEngine;
using System.Collections;

public class Mask : MonoBehaviour
{
    public MaskHolder currentOwner;

    private float flightRate = 10f;
    [HideInInspector]
    public bool midTrade = false;

    private Transform firePoint;
    
    private void OnEnable()
    {
        if (currentOwner != null)
        {
            transform.parent = currentOwner.maskHolderSpot;
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
        }
        
        firePoint = transform.GetChild(0);
    }

    //base class inheritance function
    public virtual void Ability(float input) {}

    //checks if mask is ready to fly to new owner
    public void FlyTo(MaskHolder newOwner)
    {
        //check if interactable, if not return
        if (midTrade)
            return;
        //if so, turn off interactability to begin flight process
        midTrade = true;
        
        //fly to new owner (currentOwner value)
        StartCoroutine(FlyingCoroutine(newOwner));
    }

    private IEnumerator FlyingCoroutine(MaskHolder newOwner)
    {
        //Detatch from parent
        transform.parent = null;

        Vector3 prevOwnPos = transform.position;
        
        //float timer = 0;
        //while (timer < 1)
        while (Vector3.Distance(transform.position, newOwner.maskHolderSpot.position) > 1)
        {
            transform.position = Vector3.Lerp(transform.position, newOwner.maskHolderSpot.position, Time.deltaTime * flightRate);
            //transform.position = Vector3.Lerp(prevOwnPos, newOwner.maskHolderSpot.position, timer / 1);
            //timer += Time.deltaTime;
            yield return null;
        }
        
        currentOwner = newOwner;
        transform.parent = newOwner.maskHolderSpot;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        midTrade = false;
        
        print("exit transfer");
        yield return null;
    }
    
    
}
