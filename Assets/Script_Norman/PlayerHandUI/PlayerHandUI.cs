using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerHandUI : MonoBehaviour
{
    [SerializeField] private HandUIResource handData;
    //[SerializeField] private GameObject swordHandPrefab;
    [SerializeField] private MaskHolder mholder;
    
    private bool gotMask = false;
    private Animator animator;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = handData.handPrefab.gameObject.GetComponent<Animator>();
        //Instantiate(handData.handPrefab, this.transform);
    }

    // Update is called once per frame
    void Update()
    {
        if (mholder.heldMask)
        {
            if (gotMask)
            {
                return;
            }
            animator.Play(handData.RclickAnimation.name);
            gotMask = true;
        }
        else
        {   
            //animator.Play(handData.idleAnimation.name);
        }
    }
}
