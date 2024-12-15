using UnityEngine;

namespace _Elementa
{
     class HideForDesktop : MonoBehaviour
    {

        private void Start()
        {

            if (!Application.isMobilePlatform)
            {
                gameObject.SetActive(false);
            }
        }
    }
}