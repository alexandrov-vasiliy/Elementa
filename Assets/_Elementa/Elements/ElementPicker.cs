using _Elementa.Player;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace _Elementa.Elements
{

    public class ElementPicker : MonoBehaviour
    {
        [SerializeField] private ElementData _element; // Стихия, которую этот объект дает
        [Inject] private ElementBar _elementBar; // Ссылка на полоску игрока
        public UnityEvent OnPickUp;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerBase player))
            {
                OnPickUp.Invoke();
                _elementBar.AddElement(_element); 
                Destroy(gameObject);
            }
        }
    }
}