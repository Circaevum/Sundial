using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Profiling;
using UnityEngine.UI;

public class Victron_kWh : MonoBehaviour
{
    public Root kwh_root;
    private victron_requester vic;
    public TextAsset payload;
    private LineRenderer lineRenderer;
    private Circaevum sundial;
    private GameObject clock;
    private GameObject ParentObject;
    private Slider mainSlider;
    public string body;

    public float radius;
    public long start;
    public long stop;
    public long time;
    private float inc = 12;

    public List<Tuple<long, float>> metric;
    List<Tuple<long, float>> battery_volt;
    List<Tuple<long, float>> power;
    List<Tuple<long, float>> total_kwh;

    public float lineDrawSpeed;

    void Start()
    {
        vic= GameObject.Find("Victron").GetComponent<victron_requester>();
        sundial = GameObject.Find("Sundial").GetComponent<Circaevum>();
        ParentObject = GameObject.Find("Clock");
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = 0.001f;
        lineRenderer.endWidth = 0.001f;
        mainSlider = GameObject.Find("Slider").GetComponent<Slider>();
        radius = ParentObject.GetComponent<ClockValues>().radius + 0.25f;

        string json = payload.text;
        start = metric.First().Item1;
        stop = metric.Last().Item1;

    }
    public static double UnixTimestampToAngle(long unixTimestamp)
    {
        const double millisecondsPerDay = 24 * 60 * 60 * 1000;
        double fractionOfDay = (unixTimestamp % millisecondsPerDay) / millisecondsPerDay;
        double angle = 360 * fractionOfDay;
        return angle;
    }


    void Update()
    {
        time = sundial.unix;
        //Every single increment is 5 minutes. So if 0 = Jan 1, 2020, then 144 = 12pm
        //Seemed like a reasonable place to stop as far as resolution considerations
        //5 mins is a reasonable block to schedule down to.
        lineRenderer.positionCount = metric.Count;
        for (int i = 0; i < metric.Count; i++)
        {
            float rad = metric[i].Item2*3.0f;
            float angle = (float)UnixTimestampToAngle(metric[i].Item1)-90.0f;
            long timestamp = metric[i].Item1/1000;
            //print(time+":     LOCAL"+timestamp+":   DIFF:"+(timestamp-time)+":   RAD:"+rad+ ":   ANGLE:" + angle);
            lineRenderer.SetPosition(i, new Vector3(
                - rad * Mathf.Sin(2 * Mathf.PI * angle / 360),
                - rad * Mathf.Cos(2 * Mathf.PI * angle / 360),
                (timestamp - time) / 1000000.0f * mainSlider.value/4));
        }
        float width = 0.002f * ParentObject.transform.parent.transform.localScale.x;
        lineRenderer.startWidth = width;
        lineRenderer.endWidth = width;
    }
}


