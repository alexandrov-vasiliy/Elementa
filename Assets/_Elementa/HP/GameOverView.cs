using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverView : MonoBehaviour
{
    public void RestartLevel()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        // Перезагружаем её
        SceneManager.LoadScene(currentScene.name);
        Time.timeScale = 1;

    }
}
