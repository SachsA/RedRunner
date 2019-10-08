using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField]
    private Slider VolumeSlider;

    public AudioMixer audioMixer;

    public void SetVolume()
    {
        audioMixer.SetFloat("Volume", VolumeSlider.value);
    }
}
