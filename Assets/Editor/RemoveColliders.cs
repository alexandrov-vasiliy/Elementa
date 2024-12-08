using UnityEditor;
using UnityEngine;

public class RemoveColliders : EditorWindow
{
    [MenuItem("Tools/Remove All Colliders")]
    public static void ShowWindow()
    {
        GetWindow<RemoveColliders>("Remove Colliders");
    }

    private void OnGUI()
    {
        GUILayout.Label("Удалить все коллайдеры на сцене", EditorStyles.boldLabel);

        if (GUILayout.Button("Удалить все коллайдеры"))
        {
            RemoveAllColliders();
        }
    }

    private static void RemoveAllColliders()
    {
        if (EditorUtility.DisplayDialog("Подтверждение",
                "Вы уверены, что хотите удалить все коллайдеры на текущей сцене? Это действие необратимо!",
                "Удалить", "Отмена"))
        {
            int removedCount = 0;

            Collider[] colliders = FindObjectsOfType<Collider>();
            foreach (Collider collider in colliders)
            {
                DestroyImmediate(collider);
                removedCount++;
            }

            Debug.Log($"Удалено {removedCount} коллайдеров.");
            EditorUtility.DisplayDialog("Готово", $"Удалено {removedCount} коллайдеров.", "Ок");
        }
    }
}