using _Elementa.ObjectPool;
using UnityEngine;
using Zenject;

public class SphereApproachingCamera : MonoBehaviour
{
    
    [SerializeField] private float detectionDistance = 5f; // Расстояние от края камеры для детекции
    
    
    private Renderer sphereRenderer;
    private bool hasSpawned = false;

    [Inject(Id = nameof(PoolIds.Enemies))] private ObjectPool<Enemy> enemyPool;
    private Camera _camera;

    void Start()
    {
        _camera = Camera.main;
        sphereRenderer = GetComponent<Renderer>();

        if (sphereRenderer == null)
        {
            Debug.LogError("На объекте отсутствует Renderer!");
        }
    }

    void Update()
    {
        if (!hasSpawned&&sphereRenderer != null && IsNearCamera(_camera, detectionDistance))
        {
            SpawnEnemy();
            hasSpawned = true;
        }
    }
    
    private void SpawnEnemy()
    { 
        Enemy enemy = enemyPool.Get();
        enemy.SetPosition(transform.position);
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

