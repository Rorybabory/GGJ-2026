using System;
using UnityEngine;
using UnityEngine.VFX;

public class SwordMask : Mask
{
    [SerializeField] private GameObject swordPrefab;
    [SerializeField] private GameObject disintegrateMaskPrefab;
    [SerializeField] private Transform maskModelTransform;
    [SerializeField] private Transform armRig;
    [SerializeField] private Collider swordCollider;
    [SerializeField] private VisualEffect vfx;
    private DamageSourceCollision dmc;
    private float swordtimer = 0f;
    private GameObject spawnObj;
    public override void Ability(float input)
    {
        vfx.Play();
        swordCollider.enabled = true;
        swordtimer = 0f;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dmc = swordCollider.GetComponent<DamageSourceCollision>();
        if (currentOwner.isPlayer)
        {
            
        }
        else
        {
            spawnObj = Instantiate(swordPrefab, armRig);
        }
        SelectColliderTeam();
    }

    // Update is called once per frame
    void Update()
    {
        SelectColliderTeam();
        if (swordCollider.enabled)
        {
            swordtimer += Time.deltaTime;
            if (swordtimer >= 0.1f)
            {
                swordCollider.enabled = false;
            }
        }

        if (!currentOwner.isPlayer)
        {
            
        }
    }
    
    void SelectColliderTeam()
    {
        if (currentOwner.isPlayer)
        {
            dmc.damageTeam = "Player";
        }
        else
        {
            dmc.damageTeam = "Enemy";
        }
    }

    public override void Redirect()
    {
        Destroy(spawnObj);
        /*if (currentOwner.isPlayer)
        {
            spawnObj = Instantiate(swordPrefab, swordPivot);
        }
        else
        {
            spawnObj = Instantiate(swordPrefab, armRig);
        }*/
        SelectColliderTeam();
    }

    private void OnDestroy()
    {
        if (!Application.isPlaying)
            return;

        if (!gameObject.scene.isLoaded)
            return;
        Instantiate(disintegrateMaskPrefab, maskModelTransform.position, maskModelTransform.rotation);
    }
}
