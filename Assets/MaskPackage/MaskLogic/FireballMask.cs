using System;
using UnityEngine;

public class FireballMask : Mask
{
    public GameObject fireballPrefab;
    private Transform playerTransform;
    private void Start()
    {
        playerTransform = GameObject.FindAnyObjectByType<PlayerMovement>().transform;
    }

    public override void Ability(float input)
    {
        if (currentOwner.isPlayer)
        {
            GameObject obj2 = GameObject.Instantiate(fireballPrefab, firePoint.position, firePoint.rotation);
        }
        Vector3 direction = (playerTransform.position - firePoint.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(direction);
        GameObject obj = GameObject.Instantiate(fireballPrefab, firePoint.position, rotation);
    }
}
