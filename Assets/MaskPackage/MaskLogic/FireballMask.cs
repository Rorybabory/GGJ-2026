using UnityEngine;

public class FireballMask : Mask
{
    public GameObject fireballPrefab;
    
    public override void Ability(float input)
    {
        GameObject obj = GameObject.Instantiate(fireballPrefab, firePoint.position, firePoint.rotation);
    }
}
