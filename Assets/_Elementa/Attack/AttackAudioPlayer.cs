using System.Collections;
using _Elementa.Attack.Data;
using UnityEngine;

namespace _Elementa.Attack
{
    public class AttackAudioPlayer : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        
        


        public void PlaySpawnAudio(AttackData attackData)
        {
            if (attackData.SpawnAudio == null) return;


            _audioSource.PlayOneShot(attackData.SpawnAudio);
        }

       
    }
}