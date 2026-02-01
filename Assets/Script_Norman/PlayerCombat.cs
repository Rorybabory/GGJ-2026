using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private MaskHolder _maskHolder;
    [SerializeField] private Collider _collider;

    private float timer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!_maskHolder.heldMask)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _collider.enabled = true;
            }
            else if(Input.GetMouseButtonUp(0))
            {
                _collider.enabled = false;
            }
        }
    }
}
