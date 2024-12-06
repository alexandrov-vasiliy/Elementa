using _Elementa.Enemies.Scripts;
using UnityEngine;

public class SphereApproachingCamera : MonoBehaviour
{
    
    [SerializeField] private EnemyPool enemyPool;
    [SerializeField] private float detectionDistance = 5f; // Расстояние от края камеры для детекции
    
    
    private Renderer sphereRenderer;
    private bool hasSpawned = false;

    void Start()
    {
        // Получаем компонент Renderer объекта
        sphereRenderer = GetComponent<Renderer>();

        if (sphereRenderer == null)
        {
            Debug.LogError("На объекте отсутствует Renderer!");
        }
    }

    void Update()
    {
        if (!hasSpawned&&sphereRenderer != null && IsNearCamera(Camera.main, detectionDistance))
        {
            //Debug.Log($"{gameObject.name} приближается к зоне камеры!");
            SpawnEnemy();
            hasSpawned = true;
        }
    }
    
    private void SpawnEnemy()
    { 
        GameObject enemy = enemyPool.GetEnemy();
        enemy.GetComponent<Enemy>().Activate(transform.position);
    }

    private bool IsNearCamera(Camera camera, float distance)
    {
        if (camera == null) return false;

        // Создаем расширенный фрустум камеры
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
        for (int i = 0; i < planes.Length; i++)
        {
            planes[i].distance += distance; // Увеличиваем границы на заданное расстояние
        }

        // Проверяем, находится ли объект в расширенной зоне
        return GeometryUtility.TestPlanesAABB(planes, sphereRenderer.bounds) && !IsVisibleToCamera(camera);
    }

    private bool IsVisibleToCamera(Camera camera)
    {
        if (camera == null) return false;

        // Проверяем видимость объекта в стандартной зоне камеры
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
        return GeometryUtility.TestPlanesAABB(planes, sphereRenderer.bounds);
    }
}

