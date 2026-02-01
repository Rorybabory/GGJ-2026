using UnityEngine;

public class EnemyBehaviorController : MonoBehaviour
{
    [SerializeField] private EnemyMovement enemyMoveCode;
    [SerializeField] private Animator skinAnimator;
    [SerializeField] private MaskHolder mholder;
    public static float startdelayAttack = 0.41f; // might not used
    public static float attackCooldown = 2.083f;
    [SerializeField] private float staggerDuration = 2.0f;
    private Transform playerTransform;
    private float staggerTimer = 0.0f;
    private float attackTimer = 0.0f;
    private float attackCooldownFake = 0.0f;
    private float deadTimer = 0.0f;
    private bool IsAttacking = false;
    private bool IsRunning = false;
    private bool IsDead = false;
    private bool IsStaggered = false;
    private bool once = false;
    
    void Start()
    {
        attackTimer = 100f;
        attackCooldownFake = startdelayAttack;
        playerTransform = FindAnyObjectByType<PlayerMovement>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (mholder.heldMask != null)
        {
            enemyMoveCode.range = mholder.heldMask.range;
        }
        
        if (IsDead) return;

        if (IsStaggered)
        {
            Staggered();
        }
        
        if (enemyMoveCode.FollowPlayerUntilAttackRange(IsAttacking))
        {
            IsRunning = false;
            IsAttacking = true;
        }
        else
        {
            attackTimer = 0.0f;
            attackCooldownFake = startdelayAttack;
            IsRunning = true;
            IsAttacking = false;
        }
        skinAnimator.SetBool("IsRunning", IsRunning);
        skinAnimator.SetBool("IsAttacking", IsAttacking);

        if (IsAttacking)
        {
            RotateToFacePlayer();
            if (mholder.heldMask != null)
            {
                attackTimer += Time.deltaTime;
                if (attackTimer >= attackCooldownFake)
                {
                    mholder.DoAbility();
                    attackTimer = 0f;
                    attackCooldownFake = attackCooldown;
                }
            }
        }
    }

    private void RotateToFacePlayer()
    {
        Vector3 direction = playerTransform.position - transform.position;
        direction.y = 0f; // keep enemy upright (no vertical tilt)

        if (direction.sqrMagnitude < 0.001f) return;
        
        transform.rotation = Quaternion.LookRotation(direction);
    }

    public void Staggered()
    {
        staggerTimer += Time.deltaTime;
        if (staggerDuration <= staggerTimer)
        {
            IsStaggered = false;
        }
    }

    public void DelayDead(float duration)
    {
        deadTimer += Time.deltaTime;
        if (duration <= deadTimer)
        {
            Destroy(this.gameObject);
        }
    }
}
