using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class Enemy : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    private PlayerBase _player;

    [Inject]
    public void Construct(PlayerBase playerBase)
    {
        _player = playerBase;
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    public bool HasReachedDestination()
    {
        return (_agent.remainingDistance <= (_agent.stoppingDistance + _agent.radius));
    }

    public void Attack()
    {
        _agent.SetDestination(_player.transform.position);
    }
}