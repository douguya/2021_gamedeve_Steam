using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGMSlider : MonoBehaviour
{
    private Slider slider;
    private void Awake()
    {
        slider = GetComponent<Slider>();
        slider.value = PlayerPrefs.GetFloat("BGMValue",1.0f);
    }

    public void BGMOnValueChange(float newSliderValue)
    {
        BGMManager bGmManager = BGMManager.Instance;
        bGmManager.BGMSlider(newSliderValue);
    }
}
