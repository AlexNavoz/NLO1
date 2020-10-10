using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressedButton : MonoBehaviour
{
    public bool isPressed = false;
    
    public void OnPointerUp()
    {
        isPressed = false;
    }

    public void OnPointerDown()
    {
        isPressed = true;
    }
}
