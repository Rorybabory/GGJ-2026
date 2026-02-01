using UnityEngine;

public class WEAPONDATA : MonoBehaviour
{
    [SerializeField] private MaskHolder mHolder;
    [SerializeField] private GameObject sword;
    [SerializeField] private GameObject fireball;
    [SerializeField] private GameObject shield;
    [SerializeField] private float delay;
    private float timer = 0.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (mHolder.heldMask is SwordMask)
        {
            timer += Time.deltaTime;
            if (delay <= timer)
            {
                sword.SetActive(true);
            }
            
        }
        else if (mHolder.heldMask is FireballMask)
        {
            sword.SetActive(false);
        }
        else
        {
            sword.SetActive(false);
        }
    }
}
