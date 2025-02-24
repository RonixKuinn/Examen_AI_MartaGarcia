using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum EnemyState
    {
        Patrolling,
        Chasing,
        Attacking

    }
    public EnemyState currentState;
    private UnityEngine.AI.NavMeshAgent _AIAgent;

    [SerializeField] private Transform[] _patrolPoints;
    private int _patrolIndex;

    private Transform _playerTransform;
    [SerializeField] private float _visionRange = 15;
    [SerializeField] private float _attackRange = 3;

    void Awake()
    {
        _AIAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        _playerTransform = GameObject.FindWithTag("Player").transform;
    }

    void Start()
    {
        currentState = EnemyState.Patrolling;
        SetPatrolPoint();    
    }

    void Update()
    {
        switch (currentState)
        {
            case EnemyState.Patrolling:
                Patrol();
                break;
            case EnemyState.Chasing:
                Chase();
                break;
            case EnemyState.Attacking:
                Attack();
                break;
        }
    }

    void Patrol()
    {
        if(InRage(_visionRange))
        {
            currentState = EnemyState.Chasing;
        }

        if(_AIAgent.remainingDistance < 0.5f)
        {
            SetPatrolPoint();
        }
    }

    void SetPatrolPoint()
    {
        _AIAgent.destination = _patrolPoints[Random.Range(0, _patrolPoints.Length)].position;
    }
    void Chase()
    {
        if(!InRage(_visionRange))
        {
            currentState = EnemyState.Patrolling;
        }

        if(InRage(_attackRange))
        {
            currentState = EnemyState.Attacking;
        }
        _AIAgent.destination = _playerTransform.position;
    }
   
    bool InRage(float range)
    {
        return Vector3.Distance(transform.position, _playerTransform.position) < range;
    }

    void Attack()
    {
        Debug.Log("chanclazo de madre");
        currentState = EnemyState.Chasing;
    }

}