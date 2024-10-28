using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class UI_VolumeSlider : MonoBehaviour
{
    public Slider slider;
    public string param;
    public float multiplier = 20;

    [SerializeField] AudioMixer audioMixer;

    public void SetVolume(float value)
    {
        audioMixer.SetFloat(param, Mathf.Log10(value) * multiplier);
    }

    public void SetSliderValueOnGameLoaded(float value)
    {
        if (value > 0.01f)
            slider.value = value;
    }
}
