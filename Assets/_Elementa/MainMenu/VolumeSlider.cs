using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MainMenu
{
    [RequireComponent(typeof(Slider))]
    public class VolumeSlider : MonoBehaviour
    {
        [SerializeField] private Slider _volumeSlider;
        private const string VolumePrefKey = "MasterVolume";

        private void OnValidate()
        {
            _volumeSlider ??= GetComponent<Slider>();
        }

        private void Start()
        {
            var savedVolume = PlayerPrefs.GetFloat(VolumePrefKey, 1f);
            AudioListener.volume = savedVolume;
            if (_volumeSlider == null) return;
            
            _volumeSlider.value = savedVolume;
            _volumeSlider.onValueChanged.AddListener(SetVolume);
            
        }

        public void SetVolume(float value)
        {
            AudioListener.volume = value;
            PlayerPrefs.SetFloat(VolumePrefKey, value);
        }
    }
}