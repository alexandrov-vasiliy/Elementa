using UnityEngine;

namespace _Elementa.Attack.Data
{
    public abstract class AttackData : ScriptableObject
    {
        public string AttackName;
        public float Damage;
        public float Radius = 2f;
        public float FireRate = 1f;
        public float LifeTime = 1f;
        public int AttackCount = 3;

        public AudioClip SpawnAudio;
        public AudioClip ProcessAudio;
        public AudioClip DestroyAudio;
        public abstract void ApplyEffect(GameObject target);
    }
}