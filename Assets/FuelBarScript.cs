using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelBarScript : MonoBehaviour
{
    public Slider slider;



    public void SetMaxTank(float fuel)
    {
        slider.maxValue = fuel;
        slider.value = fuel;
    }

    public void SetValue(float fuel) 
    {
        slider.value = fuel;
    }
}
