namespace _Elementa.Enemies.Scripts
{
    using System.Collections.Generic;
    using UnityEngine;

    public class EnemyPool : MonoBehaviour
    {
        [SerializeField] private GameObject enemyPrefab; // Префаб врага
        [SerializeField] private int poolSize = 10; // Размер пула

        private Queue<GameObject> enemyPool = new Queue<GameObject>();

        private void Start()
        {
            for (int i = 0; i < poolSize; i++)
            {
                GameObject enemy = Instantiate(enemyPrefab);
                enemy.SetActive(false);
                enemyPool.Enqueue(enemy);
            }
        }

        public GameObject GetEnemy()
        {
            if (enemyPool.Count > 0)
            {
                GameObject enemy = enemyPool.Dequeue();
                enemy.SetActive(true);
                return enemy;
            }
            else
            {
                GameObject enemy = Instantiate(enemyPrefab);
                enemy.SetActive(true);
                return enemy;
            }
        }

        public void ReturnEnemy(GameObject enemy)
        {
            enemy.SetActive(false);
            enemyPool.Enqueue(enemy);
        }
    }

}