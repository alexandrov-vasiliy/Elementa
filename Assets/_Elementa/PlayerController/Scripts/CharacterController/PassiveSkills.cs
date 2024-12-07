using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PassiveSkills", menuName = "Scriptable Objects/PassiveSkills")]
public class PassiveSkills : ScriptableObject
{
    [SerializeField] public List<PassiveSkill> PassiveSkillsData;

    public enum PassiveSkillsEnum
    {
        increasePlayerSpeed = 1,
        increaseHP = 2,
        increaseDamage = 3,

    }
    

    [System.Serializable]
    public struct PassiveSkill
    {
        public PassiveSkillsEnum id;
        public string name;
        public string meaning;
        public float value;
        

        public PassiveSkill(string name, PassiveSkillsEnum id, string meaning, float value)
        {
            this.id = id;
            this.name = name;
            this.meaning = meaning;
            this.value = value;
        }
    }

}
