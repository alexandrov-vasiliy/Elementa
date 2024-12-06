using UnityEngine;

namespace _Elementa.Elements
{

    public class ElementPicker : MonoBehaviour
    {
        [SerializeField] private ElementData _element; // Стихия, которую этот объект дает
        [SerializeField] private ElementBar _elementBar; // Ссылка на полоску игрока

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _elementBar.AddElement(_element); 
                Destroy(gameObject);
            }
        }
    }
}