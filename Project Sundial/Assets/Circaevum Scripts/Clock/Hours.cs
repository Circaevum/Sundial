using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Hours : MonoBehaviour
{
    public GameObject hourLabel;
    public float radius;
    // Use this for initialization
    void Start()
    {
        //hourLabel = GameObject.Find("HourLabel");
        for (int i = 1; i <= 24; i++)
        {
            InstantiateHourLabel(i - 1);
        }
    }
    public void InstantiateHourLabel(int hourNum)
    {
        GameObject hour = Instantiate(hourLabel);
        hour.transform.parent = GameObject.Find("Clock").transform;
        hour.GetComponent<HourPositioner>().hourNum = hourNum;
        hour.GetComponent<TextMeshPro>().text = hourNum.ToString();

    }
}
