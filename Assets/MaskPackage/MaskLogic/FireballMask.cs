using System;
using UnityEngine;

public class FireballMask : Mask
{
    public GameObject fireballPrefab;
    private Transform playerTransform;
    private DamageSourceCollision dmc;
    [SerializeField] private GameObject disintegrateMaskPrefab;
    [SerializeField] private Transform maskModelTransform;
    private void Start()
    {
        playerTransform = GameObject.FindAnyObjectByType<PlayerMovement>().transform;
        dmc = fireballPrefab.GetComponent<DamageSourceCollision>();
    }

    public override void Ability(float input)
    {
        dmc.damageTeam = currentOwner.gameObject.tag;
        if (currentOwner.isPlayer)
        {
            GameObject obj2 = GameObject.Instantiate(fireballPrefab, firePoint.position, firePoint.rotation);
        }
        Vector3 direction = (playerTransform.position - firePoint.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(direction);
        GameObject obj = GameObject.Instantiate(fireballPrefab, firePoint.position, rotation);
    }
    
    private void OnDestroy()
    {
        Instantiate(disintegrateMaskPrefab, maskModelTransform.position, maskModelTransform.rotation);
    }
}
