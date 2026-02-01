using UnityEngine;

public class SwordMask : Mask
{
    [SerializeField] private GameObject swordPrefab;
    [SerializeField] private Transform armRig;
    [SerializeField] private Collider swordCollider;
    private DamageSourceCollision dmc;
    private float swordtimer = 0f;
    public override void Ability(float input)
    {
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
            Instantiate(swordPrefab, armRig);
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
    }

    void SelectColliderTeam()
    {
        if (currentOwner.isPlayer)
        {
            dmc.damageTeam = LayerMask.NameToLayer("Player");
        }
        else
        {
            dmc.damageTeam = LayerMask.NameToLayer("Enemy");
        }
        
    }
}
