using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;

public class YearLines : MonoBehaviour
{

    private LineRenderer lineRenderer;
    private float counter;
    private float dist;
    private Sundial sundial;
    public float tallness;
    public Slider mainSlider;

    //public Transform origin;
    //public Transform destination;

    public Vector3[] coordinates;
    public float radius = 6f;
    
    public float lineDrawSpeed;

    //public Transform[] eventArray = new Transform[3];
    // Use this for initialization
    void Start()
    {
        sundial = GameObject.Find("Sundial").GetComponent<Sundial>();
        mainSlider = GameObject.Find("Slider").GetComponent<Slider>();
        mainSlider.onValueChanged.AddListener(delegate { Slider_Changed(mainSlider.value); });
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.useWorldSpace = false;
        sundial = GameObject.Find("Sundial").GetComponent<Sundial>();
        for (int i = 0; i < 730; i++)
        {
            lineRenderer.SetPosition(i, new Vector3(
                (sundial.distances[2] / sundial.horizonScale * Mathf.Sin(i/2f * Mathf.PI / 180 * 360f / 365f)) ,
                (i /200f * mainSlider.value),
                -(sundial.distances[2] / sundial.horizonScale * Mathf.Cos(i/2f * Mathf.PI / 180 * 360f / 365f)) ));
        }

        lineRenderer.startWidth = 0.02f;
        lineRenderer.endWidth = 0.02f;
    }

    // Update is called once per frame
    public void Slider_Changed(float newValue)
    {
        for (int i = 0; i < 730; i++)
        {
            lineRenderer.SetPosition(i, new Vector3(
                (sundial.distances[2] / sundial.horizonScale * Mathf.Sin(i / 2f * Mathf.PI / 180 * 360f / 365f)),
                (i /200f  * newValue),
                -(sundial.distances[2] / sundial.horizonScale * Mathf.Cos(i / 2f * Mathf.PI / 180 * 360f / 365f))));
        }
    }
    void Update()
    {
        /*
        counter += 0.001f;
        if (counter <= sundial.height / 33.333f)
        {
            for (int i = 0; i < 1095; i++)
            {
                lineRenderer.SetPosition(i, new Vector3(
                    (sundial.distances[2] * Mathf.Sin(i * 10 / 1.25f * Mathf.PI / 180 * 360 / 365 / 24)),
                    (i * radius / 20 / 24 / 15 * 12 * sundial.height*counter),
                    -(sundial.distances[2] * Mathf.Cos(i * 10 / 1.25f * Mathf.PI / 180 * 360 / 365 / 24))));
            }
        }
        */
    }
}
