using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace _Elementa.Elements
{


    public class ElementBar : MonoBehaviour
    {
        [SerializeField] private List<ElementData> elements = new(); 
        [SerializeField] private ElementData _airElement;
        [SerializeField] private GameObject _barItemPrefab;
        [SerializeField] private int _maxElementCount = 10;

        public event Action OnElementAdd;
        private List<GameObject> _barItems = new();
        public void AddElement(ElementData newElement)
        {
            if(elements.Count >= _maxElementCount) return;

            OnElementAdd?.Invoke();
            if (elements.Count > 0)
            {
                ElementData lastElement = elements[^1];

                var combinedElement = newElement.GetCombinationResult(lastElement);
                if (combinedElement)
                {
                    elements.Remove(lastElement);
                    elements.Add(combinedElement);
                    UpdateBar();
                    return;
                }
            }

            elements.Add(newElement);
            UpdateBar();
        }

        public ElementData GetLastElement()
        {
            if (elements.Count == 0) return _airElement;
            
            return elements[^1];
        }

        public void RemoveLastElement()
        {
            if (elements.Count > 0)
            {
                elements.RemoveAt(elements.Count - 1);
                UpdateBar();
            }
        }

        private IEnumerator AirFilling()
        {
            while (true)
            {
                
                elements.Add(_airElement);
                UpdateBar();
                yield return new WaitForSeconds(2);
            }

        }

        private void UpdateBar()
        {
            
            ClearBar();
            foreach (var elementData in elements)
            {
                var barItem = Instantiate(_barItemPrefab, transform);
                var image = barItem.GetComponent<Image>();
                image.color = elementData.Color;
                _barItems.Add(barItem);
            }
            
        }

        private void ClearBar()
        {
            foreach (var barItem in _barItems)
            {
                Destroy(barItem);
            }
        }
    }
}