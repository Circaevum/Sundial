using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;

public class StraightDayMonth : MonoBehaviour
{

    private LineRenderer lineRenderer;
    public Slider mainSlider;
    private Toggle straight;
    public int planet;
    private float counter;
    private float dist;
    public float radius;
    public int month;
    private float check;
    private Transform origin;
    private Transform earth;
    private Toggle rotateTime;

    private Sundial sundial;

    public float lineDrawSpeed;
    void Start()
    {
        origin = GameObject.Find("Orbital Plane").GetComponent<Transform>();
        earth = GameObject.Find("Earth_8K").GetComponent<Transform>();
        straight = GameObject.Find("Straight Toggle").GetComponent<Toggle>();
        straight.onValueChanged.AddListener(delegate { Start(); });
        mainSlider = GameObject.Find("Slider").GetComponent<Slider>();
        rotateTime = GameObject.Find("Toggle Rotate Time").GetComponent<Toggle>();
        sundial = GameObject.Find("Sundial").GetComponent<Sundial>();
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.useWorldSpace = false;
        
        lineRenderer.startWidth = 0.01f/sundial.horizonScale;
        lineRenderer.endWidth = 0.01f / sundial.horizonScale;
        //dist = Vector3.Distance(origin.position, destination.position);
    }
    // Update is called once per frame
    void Update()
    {
        if (straight.isOn == false && rotateTime.isOn == false)
        {
            for (int i = month * 360; i < (month + 1) * 360; i++)
            {
                lineRenderer.SetPosition(i - month * 360, new Vector3(
                    (sundial.distances[planet] / sundial.horizonScale * Mathf.Sin(i * Mathf.PI / 180 * 360f / 365f / 12f)) + (radius / sundial.horizonScale * Mathf.Sin((i+i/365) * 2 * Mathf.PI  / 12f)),
                    (i /1200f * mainSlider.value),
                    -sundial.distances[planet] / sundial.horizonScale * Mathf.Cos(i * Mathf.PI / 180 * 360f / 365f / 12f) - (radius / sundial.horizonScale * Mathf.Cos((i + i / 365) * 2 * Mathf.PI  / 12f))));
            }
        }
        else if (straight.isOn == false && rotateTime.isOn == true)
        {
            for (int i = month * 360; i < (month + 1) * 360; i++)
            {
                lineRenderer.SetPosition(i - month * 360, new Vector3(
                    sundial.rotatedShift+(sundial.distances[planet] / sundial.horizonScale * Mathf.Sin(i * Mathf.PI / 180 * 360f / 365f / 12f)) - sundial.rotatedShift*2 + (radius / sundial.horizonScale * Mathf.Sin((i + i / 365) * 2 * Mathf.PI  / 12f)),
                    (i / 1200f * mainSlider.value),
                    -sundial.distances[planet] / sundial.horizonScale * Mathf.Cos(i * Mathf.PI / 180 * 360f / 365f / 12f) - (radius / sundial.horizonScale * Mathf.Cos((i + i / 365) * 2 * Mathf.PI / 12f))));
            }
        }
        else if (straight.isOn == true && rotateTime.isOn == false)
        {
            for (int i = month * 360; i < (month + 1) * 360; i++)
            {
                lineRenderer.SetPosition(i - month * 360, new Vector3(
                    earth.position.x + (radius / sundial.horizonScale * Mathf.Sin(2 * Mathf.PI * i / 12f)),
                    ((float)i / 1200f * mainSlider.value),
                    earth.position.z - (radius / sundial.horizonScale * Mathf.Cos(2 * Mathf.PI * i / 12f))));
            }
        }
        else if (straight.isOn == true && rotateTime.isOn == true)
        {
            for (int i = month * 360; i < (month + 1) * 360; i++)
            {
                lineRenderer.SetPosition(i - month * 360, new Vector3(
                    -earth.position.y + (radius / sundial.horizonScale * Mathf.Sin(2 * Mathf.PI * i / 12f + Mathf.PI / 2f)),
                    ((float)i / 1200f * mainSlider.value),
                    earth.position.z - (radius / sundial.horizonScale * Mathf.Cos(2 * Mathf.PI * i / 12f +  Mathf.PI / 2f))));
            }
        }
    }
}
