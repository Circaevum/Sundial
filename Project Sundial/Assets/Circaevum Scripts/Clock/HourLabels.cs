using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HourLabels : MonoBehaviour
{
    public GameObject hourLabel;
    private Slider mainSlider;
    public float radius = 3;
    // Use this for initialization
    void Start()
    {
        //hourLabel = GameObject.Find("HourLabel");
        for (int i = 1; i <= 24; i++)
        {
            InstantiateHourLabel(i -1);
        }
    }
    public void InstantiateHourLabel(int hourNum)
    {
        GameObject hour = Instantiate(hourLabel);
        hour.GetComponent<HourLabelPositioner>().hourNum = hourNum;
        hour.GetComponent<TextMesh>().text = hourNum.ToString();

    }
}
