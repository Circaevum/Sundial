using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timestamp : MonoBehaviour
{
    private string timeStamp;
    private float unix;
    private string dayOfWeek;
    private string[] Weekdays = { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };
    // Start is called before the first frame update
    void Start()
    {
        unix = DateTimeOffset.Now.ToUnixTimeSeconds();
    }

    // Update is called once per frame
    void Update()
    {
        dayOfWeek = (Weekdays[(int)DateTime.Today.DayOfWeek]);
        timeStamp = DateTime.Now.ToString();
        GetComponent<TextMesh>().text = dayOfWeek + ", " + timeStamp;
    }
}
