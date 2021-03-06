﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public void SetMaxValue(float _value)
    {
        slider.maxValue = _value;
        slider.value = slider.maxValue;
        fill.color = gradient.Evaluate(1f);
    }

    public void SetValue(float _value)
    {
        slider.value = _value;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
