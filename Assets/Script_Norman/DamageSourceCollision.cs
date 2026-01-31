using System;
using UnityEngine;

public class DamageSourceCollision : MonoBehaviour
{
    [SerializeField] private LayerMask damageTeam;
    [SerializeField] private float damage;
    //[SerializeField] private float perSecond;
    
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == damageTeam) return;
        //if (perSecond > 0) return;
        Health h = other.GetComponent<Health>();
        if (h != null)
        {
            h.TakeDamage(damage);
        }
    }

    /*private void OnTriggerStay(Collider other)
    {
        if (perSecond <= 0) return;
        Health h = other.GetComponent<Health>();
        if (h != null)
        {
            h.TakeDamage(damage);
        }
    }*/
}
