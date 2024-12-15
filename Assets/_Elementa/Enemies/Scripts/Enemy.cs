using System;
using System.Collections;
using _Elementa.Enemies.Scripts;
using _Elementa.ObjectPool;
using _Elementa.Player;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class Enemy : MonoBehaviour
{
    public event Action OnDead;
    
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Health _health;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _attackRate = 1f;
    [SerializeField]  float SecondsToDestroyAfterDead = 5f;

     public float DetectRadius { get; private set; } = 20f;
     public bool IsDead { get; private set; } = false;
    
    [Inject(Id = nameof(PoolIds.Enemies))] private LazyInject<ObjectPool<Enemy>> _pool;

    private PlayerBase _player;
    private Transform _target;
    private static readonly int Attack = Animator.StringToHash(nameof(Attack));
    private static readonly int Speed = Animator.StringToHash(nameof(Speed));

    private Coroutine _attackCoroutine;
    private static readonly int Dead = Animator.StringToHash(nameof(Dead));

    [Inject]
    public void Construct(PlayerBase playerBase)
    {
        _player = playerBase;
    }

    private void OnEnable()
    {
        _health.OnDeath += HandleDeath;
        _animator = GetComponentInChildren<Animator>();
        
        _animator.SetBool(Dead, false);
        IsDead = false;

    }

    private void Update()
    {
        _animator.SetFloat(Speed, _agent.velocity.magnitude);
    }

    private void HandleDeath()
    {
        IsDead = true;
        _animator.SetBool(Dead, true);
        _agent.isStopped = true;
        if (gameObject.TryGetComponent(out SmoothFall smoothFall))
        {
            smoothFall.StartFalling();
        }
        OnDead?.Invoke();
        StartCoroutine(ReturnToPoolDelayed());
    }

    private IEnumerator ReturnToPoolDelayed()
    {
        yield return new WaitForSeconds(SecondsToDestroyAfterDead);
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

    public void SetTarget(Transform target)
    {
        _target = target;
        _agent.SetDestination(target.position);
        _agent.isStopped = false;

    }

    public void StartAttack()
    {
        if (_attackCoroutine == null)
        {
            _attackCoroutine = StartCoroutine(AttackRoutine());
        }
    }

    private IEnumerator AttackRoutine()
    {
        while (true)
        {
            _animator.SetTrigger(Attack);
    
            yield return new WaitForSeconds(_attackRate); 
        }
    }

   
  
   

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, DetectRadius);

        Gizmos.color = Color.yellow;
    }

    public void StopAttack()
    {
        if (_attackCoroutine != null)
        {
            StopCoroutine(_attackCoroutine);
            _attackCoroutine = null; // Останавливаем корутину атаки
        }
    }

    public void Agr()
    {
        SetTarget(_player.transform);
    }
}