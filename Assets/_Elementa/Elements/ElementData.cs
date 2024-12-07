using System.Collections.Generic;
using _Elementa.Attack.Data;
using UnityEngine;

namespace _Elementa.Elements
{
   
    [System.Serializable]
    public class CombinationRule
    {
        public ElementData otherElement; 
        public ElementData result;     
    }
    
    [CreateAssetMenu(fileName = "New Element", menuName = "Elements/ElementData")]
    public class ElementData : ScriptableObject
    {
        public EElements ElementName;       
        public Color Color;              
        public Sprite Icon;          
        public AttackData AttackData;

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