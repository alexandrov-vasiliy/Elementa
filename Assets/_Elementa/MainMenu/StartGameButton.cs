using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.MainMenu
{

    [RequireComponent(typeof(Button))]
    public class StartGameButton : MonoBehaviour
    {
        public string sceneName;

        private void Awake()
        {
            Button button = GetComponent<Button>();
            button.onClick.AddListener(OnButtonClick);
        }

        private void OnButtonClick()
        {
            if (sceneName != null)
            {
               SceneManager.LoadScene(sceneName);
            }
            else
            {
                Debug.LogWarning("Scene is not set!");
            }
        }
    }

}