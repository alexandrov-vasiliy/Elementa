using UnityEngine;

namespace _Elementa
{
    public class FindEnemy
    {
        private readonly LayerMask _enemyLayer;

        public FindEnemy(LayerMask enemyLayer)
        {
            _enemyLayer = enemyLayer;
        }

        public Transform Nearest(Vector3 position, float searchRadius)
        {
            Collider[] hitColliders = Physics.OverlapSphere(position, searchRadius, _enemyLayer);
            float closestDistance = float.MaxValue;
            Transform nearestEnemy = null;

            foreach (var collider in hitColliders)
            {
                if (collider.TryGetComponent(out Enemy enemy) && enemy.IsDead == false)
                {
                    float distance = Vector3.Distance(position, enemy.transform.position);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        nearestEnemy = enemy.transform;
                    }
                }
            }

            return nearestEnemy;
        }
    }
}