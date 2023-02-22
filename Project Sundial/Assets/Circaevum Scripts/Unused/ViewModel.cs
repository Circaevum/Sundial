using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewModel : MonoBehaviour {

    public Text buttonText;
    public float height;

    public void Slider_Changed(float newValue)
    {
        Debug.Log(newValue);
        height = newValue;
    }
}
