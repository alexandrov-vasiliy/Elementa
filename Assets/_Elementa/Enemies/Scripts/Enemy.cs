using UnityEngine;

public class Enemy : MonoBehaviour
{
    public void Activate(Vector3 position)
    {
        transform.position = position;
        gameObject.SetActive(true);
    }
}

