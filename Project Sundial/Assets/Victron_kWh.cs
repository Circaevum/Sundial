using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.UI;

public class Victron_kWh : MonoBehaviour
{
    public TextAsset payload;
    private LineRenderer lineRenderer;
    private Circaevum sundial;
    private GameObject clock;
    private GameObject ParentObject;
    private Slider mainSlider;
    public string body;

    public float radius;
    public int start;
    public int stop;
    public int time;
    private float inc = 12;

    private List<(int,float)> battery = new List<(int, float)>();
    private List<(int,float)> solar = new List<(int, float)>();
    private List<(int,float)> alternator = new List<(int, float)>();
    private List<(int,float)> dc_load = new List<(int, float)>();
    private List<(int,float)> ac_load = new List<(int, float)>();

    public float lineDrawSpeed;

    void Start()
    {
        sundial = GameObject.Find("Sundial").GetComponent<Circaevum>();
        ParentObject = GameObject.Find("Clock");
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = 0.001f;
        lineRenderer.endWidth = 0.001f;
        mainSlider = GameObject.Find("Slider").GetComponent<Slider>();
        radius = ParentObject.GetComponent<ClockValues>().radius + 0.25f;

        string json = payload.text;


        Root iStats = JsonConvert.DeserializeObject<Root>(json);
        print("ZZZ:" + UnixTimestampToAngle((int)iStats.records.Bc[0][0]));
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
        time = sundial.now;
        start = time - 5 * 288;
        stop = time + 5 * 288;
        //Every single increment is 5 minutes. So if 0 = Jan 1, 2020, then 144 = 12pm
        //Seemed like a reasonable place to stop as far as resolution considerations
        //5 mins is a reasonable block to schedule down to.
        int i = 0;
        lineRenderer.positionCount = Mathf.Abs(stop - start);
        for (float j = start; j < stop; j++)
        {
            lineRenderer.SetPosition(i, new Vector3(
                (radius - 0.1f) * Mathf.Sin(Mathf.PI * (2 * j / 24f / inc) - Mathf.PI),
                (radius - 0.1f) * Mathf.Cos(Mathf.PI * (2 * j / 24f / inc) - Mathf.PI),
                (j - time) / 240f / inc * mainSlider.value / 4));
            i++;
        }
        float width = 0.002f * ParentObject.transform.parent.transform.localScale.x;
        lineRenderer.startWidth = width;
        lineRenderer.endWidth = width;
    }
}
[System.Serializable]
public class Root
{
    public bool success { get; set; }
    public Records records { get; set; }
    public Totals totals { get; set; }
}
[System.Serializable]
public class Records
{
    public List<List<double>> Bc { get; set; }
    public List<List<double>> Pb { get; set; }
    public List<List<double>> Pc { get; set; }
    public List<List<double>> kwh { get; set; }
}
[System.Serializable]
public class Totals
{
    public double Bc { get; set; }
    public double Pb { get; set; }
    public double Pc { get; set; }
    public double kwh { get; set; }
}


