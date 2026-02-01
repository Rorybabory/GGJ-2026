using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private MaskHolder _maskHolder;
    [SerializeField] private Collider _collider;
    [SerializeField] private GameObject hand1;
    [SerializeField] private GameObject hand2;

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
                hand1.SetActive(true);
                hand2.SetActive(false);
            }
            else if(Input.GetMouseButtonUp(0))
            {
                _collider.enabled = false;
                hand1.SetActive(false);
                hand2.SetActive(true);
            }
        }
    }
}
