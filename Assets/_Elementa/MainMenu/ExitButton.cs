using UnityEngine;
using UnityEngine.UI;

namespace UI.MainMenu
{
    [RequireComponent(typeof(Button))]
    public class ExitGameButton : MonoBehaviour
    {
        private void Awake()
        {
            Button button = GetComponent<Button>();
            button.onClick.AddListener(ExitGame);
        }

        private void ExitGame()
        {
            Debug.Log("Exiting game...");
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
        }
    }
}