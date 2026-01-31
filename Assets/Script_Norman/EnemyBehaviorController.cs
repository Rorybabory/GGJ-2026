using UnityEngine;

public class EnemyBehaviorController : MonoBehaviour
{
    [SerializeField] private EnemyMovement enemyMoveCode;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        enemyMoveCode.FollowPlayer();
    }
}
