using Unity.VisualScripting;
using UnityEngine;

public class DefaultHand : MonoBehaviour
{
    [SerializeField] private MaskHolder maskHolder;
    [SerializeField] private AnimationClip stealAnimation;
    [SerializeField] private AnimationClip idleAnimation;
    
    private bool gotMask = false;
    Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (maskHolder.heldMask)
        {
            if (gotMask)
            {
                return;
            }
            animator.Play(stealAnimation.name);
            gotMask = true;
        }
        else
        {
            if (!gotMask)
            {
                return;
            }
            animator.Play(idleAnimation.name);
            gotMask = false;
        }
    }
}
