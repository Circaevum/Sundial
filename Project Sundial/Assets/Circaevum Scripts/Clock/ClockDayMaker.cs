using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ClockDayMaker : MonoBehaviour
{
    public GameObject clockDay;
    private Sundial sundial;
    public float radius;
    private int theTime;
    // Use this for initialization
    void Start()
    {
        sundial = GameObject.Find("Sundial").GetComponent<Sundial>();
        //Determine the local time.
        theTime = sundial.now;

        //Find position of the start of today
        theTime -= (theTime % 288);

        //Instantiate 10 labels into in future and past
        for (int i = -10; i <= 10; i++)
        {
            InstantiateDayLabel(DateTime.Now.AddDays(i),i);
        }
    }
    public void InstantiateDayLabel(DateTime dayValue,int adder)
    {
        GameObject day = Instantiate(clockDay);
        day.transform.parent = GameObject.Find("Clock").transform;
        day.GetComponent<ClockDayPositioner>().date = dayValue;
        day.GetComponent<ClockDayPositioner>().adder = adder;

    }
}