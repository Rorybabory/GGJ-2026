using System;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [field: SerializeField] 
    public float currHealth { get; private set; }

    public bool isPlayer = false;
    
    [SerializeField] private float maxHealth;
    public float GetMaxHealth(){ return maxHealth; }
    
    public UnityEvent OnDamaged;
    public UnityEvent OnStaggered;
    public UnityEvent OnDead;
    
    private bool hasStaggered = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currHealth = maxHealth;
    }

    private void Update()
    {
        if (currHealth == 0 && !hasStaggered)
        {
            hasStaggered = true;
            if (isPlayer)
            {
                OnDead.Invoke();
                return;
            }
            OnStaggered.Invoke();
            
        }else if (currHealth < 0)
        {
            OnDead.Invoke();
        }
    }

    public void TakeDamage(float damage)
    {
        if (currHealth > 0 && currHealth < damage)
        {
            currHealth = 0;
        }
        else
        {
            currHealth -= damage;
        }
        
        OnDamaged.Invoke();
    }

    public void Kill()
    {
        currHealth = -3;
    }
}
