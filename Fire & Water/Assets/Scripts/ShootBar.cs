using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public void SetMaxTime(float _value)
    {
        slider.maxValue = _value;
        slider.value = slider.maxValue;
        fill.color = gradient.Evaluate(1f);
    }

    public void SetTime(float _value)
    {
        slider.value = _value;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
