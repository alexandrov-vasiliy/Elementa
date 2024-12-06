using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace _Elementa.Elements
{


    public class ElementBar : MonoBehaviour
    {
        [SerializeField] private Image barImage; // Полоска (например, Image компонент в UI)
        [SerializeField] private List<ElementData> elements = new List<ElementData>(); // Текущие элементы в полоске
        [SerializeField] private float maxFill = 1f; // Максимальное заполнение полоски
        [SerializeField] private ElementData _airElement;
        public void AddElement(ElementData newElement)
        {
            if (elements.Count > 0)
            {
                // Проверяем последнюю добавленную стихию
                ElementData lastElement = elements[elements.Count - 1];

                // Если есть комбинация, заменяем последнюю стихию на результат
                var result = newElement.GetCombinationResult(newElement);
                if (result)
                {
                    
                    elements[^1] = result;
                    UpdateBar();
                    return;
                }
            }

            // Если комбинации нет, добавляем элемент
            elements.Add(newElement);
            UpdateBar();
        }

        private void Start()
        {
            StartCoroutine(AirFilling());
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
            // Обновляем цвет полоски на основе элементов
            if (elements.Count == 0)
            {
                barImage.color = Color.clear; // Полоска пустая
                return;
            }

            // Создаем градиент на основе добавленных элементов
            Texture2D texture = new Texture2D(elements.Count, 1);
            for (int i = 0; i < elements.Count; i++)
            {
                texture.SetPixel(i, 0, elements[i].Color);
            }
            texture.Apply();

            // Обновляем цветовой градиент полоски
            barImage.material = new Material(Shader.Find("UI/Default"));
            barImage.material.mainTexture = texture;
        }
    }
}