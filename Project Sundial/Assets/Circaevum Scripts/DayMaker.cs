using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DayMaker : MonoBehaviour
{
    public GameObject dayX;
    private Sundial sundial;
    // Use this for initialization
    void Start()
    {
        sundial = GameObject.Find("Sundial").GetComponent<Sundial>();
        for (int i = 0; i < 365; i++)
        {
            InstantiateDay(i);
        }
        dayX = GameObject.Find("Day");
    }
    public void InstantiateDay(int julianDay)
    {
        GameObject day = Instantiate(dayX);
        day.transform.parent = GameObject.Find("Orbit").transform;
        day.GetComponent<DayLocater>().julianDay = julianDay;
        day.name = "CIRCA_"+julianDay;
    }
}