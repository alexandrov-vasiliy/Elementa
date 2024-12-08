using System;
using System.Collections;
using _Elementa.ObjectPool;
using _Elementa.Player;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class Enemy : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Health _health;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _attackRate = 1f;
    [SerializeField] private float _attackDamage = 20f;
    [SerializeField] private float _attackRadius = 1f;
    [SerializeField] private Transform _attackPoint;
     public float DetectRadius { get; private set; } = 20f;
    
    [Inject(Id = nameof(PoolIds.Enemies))] private LazyInject<ObjectPool<Enemy>> _pool;

    private PlayerBase _player;
    private Transform _target;
    private static readonly int Attack = Animator.StringToHash(nameof(Attack));
    private static readonly int Speed = Animator.StringToHash("Speed");

    private Coroutine _attackCoroutine;
    
    [Inject]
    public void Construct(PlayerBase playerBase)
    {
        _player = playerBase;
    }

    private void OnEnable()
    {
        _health.OnDeath += HandleDeath;
        _animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        _animator.SetFloat(Speed, _agent.velocity.magnitude);
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

    public void SetTarget(Transform target)
    {
        _target = target;
        _agent.SetDestination(target.position);
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

            DealDamage();
            yield return new WaitForSeconds(_attackRate); 
        }
    }

    private IEnumerator DealDamgeWithDelay()
    {
        yield return new WaitForSeconds(0.5f);
        DealDamage();
    }

    private void DealDamage()
    {
        Collider[] hitObjects = Physics.OverlapSphere(_attackPoint.position, _attackRadius);

        foreach (var hit in hitObjects)
        {
            if (hit.gameObject != gameObject && hit.GetComponent<Enemy>() == null)
            {
                IDamageable damageable = hit.GetComponent<IDamageable>();
                if (damageable != null)
                {
                    damageable.Damage(_attackDamage);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_attackPoint.position, _attackRadius);
        Gizmos.color = Color.red;
       
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