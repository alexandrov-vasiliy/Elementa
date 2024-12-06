using System.Collections.Generic;
using UnityEngine;

namespace _Elementa.Elements
{
   
    [CreateAssetMenu(fileName = "New Element", menuName = "Elements/ElementData")]
    public class ElementData : ScriptableObject
    {
        public string ElementName;       
        public Color Color;              
        public Sprite Icon;              
        [System.Serializable]
        public class CombinationRule
        {
            public ElementData otherElement; // С каким элементом можно комбинировать
            public ElementData result;       // Результат комбинации
        }
    
        public List<CombinationRule> combinationRules = new List<CombinationRule>();  
        
        public ElementData GetCombinationResult(ElementData other)
        {
            foreach (var rule in combinationRules)
            {
                if (rule.otherElement == other)
                    return rule.result;
            }
            return null; // Если нет подходящей комбинации
        }
    }

}