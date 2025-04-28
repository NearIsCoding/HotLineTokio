using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio; // <- Para el AudioMixer
using UnityEngine.UI;    // <- Para el Slider

public class Volumen : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider slider; 

    public string parameterName = "MyExposedParam";

    void Start()
    {
        if (slider != null)
        {
            slider.onValueChanged.AddListener(SetVolume);
        }
    }

    void SetVolume(float value)
    {
        mixer.SetFloat(parameterName, Mathf.Log10(Mathf.Max(value, 0.0001f)) * 20);
    }
}
