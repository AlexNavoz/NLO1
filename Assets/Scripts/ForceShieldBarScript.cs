using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ForceShieldBarScript : MonoBehaviour
{
    Slider slider;

    private void Start()
    {
        slider = GameObject.FindGameObjectWithTag("ForceShieldBar").GetComponent<Slider>();
    }
    public void SetHPValue(float health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetValue(float health)
    {
        slider.value = health;
    }
}
