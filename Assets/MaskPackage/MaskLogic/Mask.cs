using System;
using UnityEngine;
using System.Collections;
using AudioSystem;
using UnityEngine.Events;

public class Mask : MonoBehaviour
{
    public MaskHolder currentOwner;

    private float flightRate = 10f;
    private float duration = 0.5f;
    private float timer = 0.0f;
    
    [HideInInspector]
    public bool midTrade = false;

    public float range;

    protected Transform firePoint;
    public UnityEvent ObtainWeapon; // when the mask hit

    private void OnEnable()
    {
        if (currentOwner != null)
        {
            transform.parent = currentOwner.maskHolderSpot;
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            currentOwner.heldMask = this;
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
        this.Redirect();
        //fly to new owner (currentOwner value)
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlayAudio("mask_pull");
        }
        StartCoroutine(FlyingCoroutine(newOwner));
    }

    private IEnumerator FlyingCoroutine(MaskHolder newOwner)
    {
        //Detatch from parent
        transform.parent = null;

        Vector3 prevOwnPos = transform.position;
        Vector3 startPos = transform.position;
        
        timer = 0;
        while (timer < duration)
        //while (Vector3.Distance(transform.position, newOwner.maskHolderSpot.position) > 1)
        {
            timer += Time.deltaTime;
            transform.position = Vector3.Lerp(startPos, newOwner.maskHolderSpot.position, EaseInCirc(timer / duration));
            //transform.position = Vector3.Lerp(transform.position, newOwner.maskHolderSpot.position, Time.deltaTime * flightRate);
            
            //transform.position = Vector3.Lerp(prevOwnPos, newOwner.maskHolderSpot.position, timer / 1);
            //timer += Time.deltaTime;
            //Debug.Log(timer/duration);
            yield return null;
        }
        transform.position = newOwner.maskHolderSpot.position;
        ObtainWeapon.Invoke();
        currentOwner = newOwner;
        transform.parent = newOwner.maskHolderSpot;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        timer = 0.0f;
        midTrade = false;
        
        print("exit transfer");
        yield return null;
    }

    private float EaseInOutQuart(float t)
    {
        return t < 0.5 ? 8 * t * t * t * t : 1.0f - Mathf.Pow(-2f * t + 2f, 4f)/2f;
        //return x < 0.5 ? 8 * x * x * x * x : 1 - Math.pow(-2 * x + 2, 4) / 2;
    }
    
    private float EaseInOutQuad(float t)
    {
        return t < 0.5 ? 2 * t * t : 1.0f - Mathf.Pow(-2f * t + 2f, 2f)/2f;
        //return x < 0.5 ? 2 * x * x : 1 - Math.pow(-2 * x + 2, 2) / 2;
    }

    private float EaseOutExpo(float t)
    {
        return t >= 0.99f ? 1f : 1f - Mathf.Pow(2f, -10f * t);
    }

    private float EaseInCirc(float t)
    {
        return 1 - Mathf.Sqrt(1f - Mathf.Pow(t, 2));
        //return 1 - Math.sqrt(1 - Math.pow(x, 2));
    }

    public virtual void Redirect()
    {
        
    }
}
