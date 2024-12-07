using System;
using _Elementa.ObjectPool;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class Enemy : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Health _health;

    [Inject(Id = nameof(PoolIds.Enemies))] private LazyInject<ObjectPool<Enemy>> _pool;

    private PlayerBase _player;
    private Transform _target;

    
    [Inject]
    public void Construct(PlayerBase playerBase)
    {
        _player = playerBase;
    }

    private void OnEnable()
    {
        _health.OnDeath += HandleDeath;
    }

    private void HandleDeath()
    {
        _pool.Value.ReturnToPool(this);
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