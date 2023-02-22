using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
public class ClockDayPositioner : MonoBehaviour
{
    private Sundial sundial;
    public GameObject ParentObject;
    public GameObject ClockTime;
    private Slider mainSlider;
    public DateTime date;
    public int adder;
    private float radius;
    public int theTime;
    public int dayTime=0;
    private int diff;
    // Use this for initialization
    void Start()
    {
        //Need to reference Planetary Parameters to fill in Month Label
        string[] DayString = date.ToString().Split('/');
        sundial = GameObject.Find("Sundial").GetComponent<Sundial>();
        string monthName = sundial.MonthArray[Convert.ToInt32(DayString[0])-1];
        name = DayString[1];
        GetComponent<TMP_Text>().text = DayString[1] + "\n" + monthName;

        //Set other constants for this particular day label
        dayTime = sundial.DATE2FIVE(date.ToString().Split(' ')[0]);
        radius = ParentObject.transform.parent.GetComponent<ClockValues>().radius + 0.15f;
        mainSlider = GameObject.Find("Slider").GetComponent<Slider>();
    }
    void Update()
    {
        //As the user scrolls forward and backwards in time, reposition the day labels to follow the right time
        theTime = sundial.now;
        int diff = dayTime - theTime;
        transform.position = GameObject.Find("ClockDays").GetComponent<Transform>().position + new Vector3(
            0,
            radius * 0.6f,
            mainSlider.value * (diff + 144) / 288 / 24 * 3 / 5//(theTime % 288) * 288
            );

        //Face the day labels towards the user
        transform.LookAt(Camera.main.transform);
    }
}
