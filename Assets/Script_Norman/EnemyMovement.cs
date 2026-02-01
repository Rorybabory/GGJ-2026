using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    public float range;
    private Transform playerTransform;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public bool FollowPlayerUntilAttackRange(bool IsAttacking)
    {
        if (Vector3.Distance(transform.position, playerTransform.position) <= range)
        {
            agent.SetDestination(transform.position);
            return true;
        }

        if (!IsAttacking)
        {
            agent.SetDestination(playerTransform.position);
        }
        return false;
    }

    private void Update()
    {
        
    }
}
