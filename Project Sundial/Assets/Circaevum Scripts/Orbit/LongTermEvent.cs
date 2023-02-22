using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;

public class LongTermEvent : MonoBehaviour
{

    private LineRenderer lineRenderer;
    private Slider mainSlider;
    public GameObject clock;
    public GameObject ParentObject;
    private Sundial sundial;
    public string body;

    public float radius;
    public float category;
    public int start;
    public int stop;
    public float time;
    private float inc = 12;

    public float lineDrawSpeed;

    //public Transform[] eventArray = new Transform[3];
    // Use this for initialization
    void Start()
    {
        mainSlider = GameObject.Find("Slider").GetComponent<Slider>();
        sundial = GameObject.Find("Sundial").GetComponent<Sundial>();
        int i = 0;
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.useWorldSpace = false;
        lineRenderer.startWidth = 0.01f;
        lineRenderer.endWidth = 0.01f;
        radius = ParentObject.GetComponent<ClockValues>().radius+category;

    }
    // Update is called once per frame

    void Update()
    {
        time = sundial.now;
        //Every single increment is 5 minutes. So if 0 = Jan 1, 2020, then 144 = 12pm
        //Seemed like a reasonable place to stop as far as resolution considerations
        //5 mins is a reasonable block to schedule down to.
        int i = 0;
        if (Mathf.Abs((start - time) / 240f / inc * mainSlider.value / 4) <= 5)
        {
            lineRenderer.SetVertexCount(Mathf.Abs(stop - start));
            for (int j = start; j < stop; j++)
            {
                lineRenderer.SetPosition(i, new Vector3(
                    0,
                    radius*1.2f,
                    (j - time ) / 240f / inc * mainSlider.value / 4));
                i++;
            }
        }
        else
        {
            lineRenderer.SetVertexCount(0);
        }
    }
}
