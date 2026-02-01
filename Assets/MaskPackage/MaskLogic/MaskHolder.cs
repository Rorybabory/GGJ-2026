using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.LowLevelPhysics;
using System.Collections;
using AudioSystem;
using UnityEngine.Events;

public class MaskHolder : MonoBehaviour
{
    [Tooltip("only applies to player")]
    public float cooldownTime;
    [Tooltip("to ensure raycast only hits mask, only applies to player")]
    public LayerMask castLayerMask;
    public Mask heldMask = null;
    public GameObject heldMaskObj = null;
    public Transform maskHolderSpot;
    
    [HideInInspector]
    public bool transferring = false;
    [HideInInspector]
    public bool cooldownReady = true;

    private float castRadius = 1;
    private float castDistance = 100f;
    [HideInInspector]
    public bool isPlayer;
    [HideInInspector]
    public Transform cam;
    [HideInInspector]
    public Rigidbody rb;

    [HideInInspector]
    public Vector3 velocity;
    private Vector3 previousPos;
    public bool isStaggered = false;

    public UnityEvent UseWeapon;

    private float delaydiscard = 0.2f;
    private float timer = 0.0f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (transform.gameObject.CompareTag("Player"))
        {
            isPlayer = true;
            cam = GameObject.FindGameObjectWithTag("MainCamera").transform;
        }
        else
        {
            isPlayer = false;
            cam = null;
        }        
        transform.position = maskHolderSpot.position;
        rb = GetComponent<Rigidbody>();
        cooldownReady = true;
    }

    private void Update()
    {
        velocity = (transform.position - previousPos) / Time.deltaTime;
        previousPos = transform.position;

        if (isStaggered)
        {
            IfStaggeredAndStealMask();
        }
    }

    private void OnAbility(InputValue value)
    {
        if (!heldMask)
            return;
        UseWeapon.Invoke();
        switch (heldMask)
        {
            case FireballMask:
                AudioManager.instance.PlayAudio("shoot_fireball");
                break;
            case SwordMask:
                AudioManager.instance.PlayAudio("sword_miss");
                break;
        }
        heldMask.Ability(value.Get<float>());
    }

    private void OnDiscard(InputValue value)
    {
        if (heldMaskObj != null && !heldMask.midTrade)
        {
            Discard();
            return;
        }
    }

    private void OnSteal(InputValue value)
    {
        /*if (heldMaskObj != null && !heldMask.midTrade)
        {
            timer += Time.deltaTime;
        }
        
        if (heldMaskObj != null && !heldMask.midTrade && timer >= delaydiscard)
        {
            Discard();
            timer = 0.0f;
            return;
        }*/

        if (!cooldownReady)
            return;
        cooldownReady = false;
        
        
        
        Ray ray = new Ray();
        ray.origin = cam.position;
        ray.direction = cam.forward;
        //Cast to find stealable mask
        //if (Physics.SphereCast(ray, castRadius, out RaycastHit hit, castDistance, castLayerMask))
        if (Physics.Raycast(ray, out RaycastHit hit, castDistance, castLayerMask))
        {
            if (hit.collider.gameObject.layer != LayerMask.NameToLayer("Mask"))
            {
                cooldownReady = true;
                return;
            }
            
            MaskHolder mhd = hit.collider.gameObject.GetComponentInParent<MaskHolder>();
            if (mhd != null)
            {
                if (!mhd.isStaggered)
                {
                    cooldownReady = true;
                    return;
                }
            }
            //Debug.Log(hit.collider.gameObject.name);
            if (hit.collider.gameObject.GetComponent<Mask>().currentOwner == null)
            {
                return;
            }else if (hit.collider.gameObject.GetComponent<Mask>().currentOwner.GetComponent<MaskHolder>() == null)
            {
                return;
            }

            if (heldMask is FireballMask || heldMask is ShieldMask || heldMask is SwordMask)
            {
                GameObject obj = heldMask.gameObject;
                heldMask = null;
                obj.transform.parent = null;
                GameObject.Destroy(obj);
            }
            
            heldMask = hit.collider.GetComponent<Mask>();
            heldMask.currentOwner.GetComponent<MaskHolder>().heldMask = heldMask;
            heldMask.currentOwner.heldMask = null;
            heldMaskObj = heldMask.gameObject;
            heldMask.FlyTo(this);
            StartCoroutine(CooldownFunction());
            return;
        }
        
        cooldownReady = true;
        return;
    }

    private void Discard()
    {
        print("enter discard");
        GameObject obj = heldMask.gameObject;
        heldMask = null;
        obj.transform.parent = null;
        GameObject.Destroy(obj);
    }

    public void CycleCooldown()
    {
        if(!cooldownReady)
            return;
        cooldownReady = false;
        
        StartCoroutine(CooldownFunction());
    }

    private IEnumerator CooldownFunction()
    {
        print("cooldown function");
        yield return new WaitForSeconds(cooldownTime);
        cooldownReady = true;
    }

    public void SetIsStaggered(bool isStaggered)
    {
        this.isStaggered = isStaggered;
    }
    
    private void IfStaggeredAndStealMask()
    {
        if (heldMask != null || isPlayer)
        {
            return;
        }

        Health h = GetComponent<Health>();
        h.Kill();
    }

    public void DoAbility()
    {
        heldMask.Ability(1); // 0 is placeholder
    }
}
