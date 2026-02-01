using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.LowLevelPhysics;
using System.Collections;

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
        
        heldMask.Ability(value.Get<float>());
    }

    private void OnSteal(InputValue value)
    {
        if (heldMaskObj != null && !heldMask.midTrade)
        {
            Discard();
            return;
        }
        
        if (!cooldownReady)
            return;
        cooldownReady = false;

        print("enmter steal");
        
        
        Ray ray = new Ray();
        ray.origin = cam.position;
        ray.direction = cam.forward;
        //Cast to find stealable mask
        if (Physics.SphereCast(ray, castRadius, out RaycastHit hit, castDistance, castLayerMask))
        {
            MaskHolder mhd = hit.collider.gameObject.GetComponentInParent<MaskHolder>();
            if (mhd != null)
            {
                if (!mhd.isStaggered)
                {
                    Debug.Log(hit.collider.gameObject.name);
                    return;
                }
            }

            heldMask = hit.transform.GetComponent<Mask>();
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
}
