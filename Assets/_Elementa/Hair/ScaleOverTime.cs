using UnityEngine;

public class ScaleOverTime : MonoBehaviour
{
    [SerializeField] private Vector3 scaleSpeed = new Vector3(0.1f, 0.1f, 0.1f); // Скорость увеличения размера
    [SerializeField] private Vector3 maxScale = new Vector3(3f, 3f, 3f); // Максимальный размер

    private void Update()
    {
        // Увеличиваем размер объекта
        transform.localScale += scaleSpeed * Time.deltaTime;

        // Ограничиваем размер, чтобы он не превышал maxScale
        transform.localScale = new Vector3(
            Mathf.Min(transform.localScale.x, maxScale.x),
            Mathf.Min(transform.localScale.y, maxScale.y),
            Mathf.Min(transform.localScale.z, maxScale.z)
        );
    }
}