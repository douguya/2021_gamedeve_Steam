using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SESlider : MonoBehaviour
{
    private Slider slider;
    private void Awake()
    {
        slider = GetComponent<Slider>();
        slider.value = PlayerPrefs.GetFloat("SEValue",1.0f);
    }

    public void SEOnValueChange(float newSliderValue)
    {
        SEManager sEManager = SEManager.Instance;
        sEManager.SESlider(newSliderValue);
    }
}
