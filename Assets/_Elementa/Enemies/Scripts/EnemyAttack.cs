using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float _attackDamage = 20f;
    [SerializeField] private float _attackRadius = 1f;
    [SerializeField] private Transform _attackPoint;
    
    public void DealDamage()
    {
        Debug.Log("EnemyDealDamage");
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
}
