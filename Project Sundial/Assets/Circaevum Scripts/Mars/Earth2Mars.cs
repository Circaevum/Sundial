using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;

public class Earth2Mars : MonoBehaviour
{

    private LineRenderer lineRenderer;
    private float counter;
    private float dist;
    private Slider mainSlider;
    private Toggle straight;
    private Transform origin;
    private Transform earth;
    private Transform mars;
    private Toggle rotateTime;


    private Sundial sundial;

    //public Transform origin;
    //public Transform destination;
    
    public int start;
    public int stop;
    private float inc = 12;
    private float checkHeight;

    public float lineDrawSpeed;

    //public Transform[] eventArray = new Transform[3];
    // Use this for initialization
    void Start()
    {
        sundial = GameObject.Find("Sundial").GetComponent<Sundial>();
        origin = GameObject.Find("Y-Origin").GetComponent<Transform>();
        earth = GameObject.Find("Earth_16K_gamma").GetComponent<Transform>();
        mars = GameObject.Find("Mars_gamma").GetComponent<Transform>();
        straight = GameObject.Find("Straight Toggle").GetComponent<Toggle>();
        rotateTime = GameObject.Find("Toggle Rotate Time").GetComponent<Toggle>();
        mainSlider = GameObject.Find("Slider").GetComponent<Slider>();
        int i = 0;
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.useWorldSpace = false;
        lineRenderer.SetVertexCount(stop - start);
        lineRenderer.startWidth = 1f;
        lineRenderer.endWidth = 1f;
        checkHeight = mainSlider.value;

    }
    // Update is called once per frame

    void Update()
    {
        float offset = 7.9f / 9;
        if (straight.isOn == false && rotateTime.isOn == false)
        {
            int i = 0;
            for (int j = start; j < stop; j++)
            {
                lineRenderer.SetPosition(i, new Vector3(
                     ((sundial.distances[2] + (sundial.distances[3]-sundial.distances[2])/(stop-start)*i) * Mathf.Sin(j * offset * Mathf.PI / 180 * 360 / 365 / 24 / inc)),
                     ((float)j * 6 / 4800f / inc * (360f / 365f) * mainSlider.value),
                     -((sundial.distances[2] + (sundial.distances[3] - sundial.distances[2]) / (stop - start) * i) * Mathf.Cos(j * offset * Mathf.PI / 180 * 360 / 365 / 24 / inc))));
                i++;
            }
        }
        else if (straight.isOn == false && rotateTime.isOn == true)
        {
            int i = 0;
            for (int j = start; j < stop; j++)
            {
                lineRenderer.SetPosition(i, new Vector3(
                     ((float)j * 6 / 4800f / inc * (360f / 365f) * mainSlider.value),
                     -((sundial.distances[2] + (sundial.distances[3] - sundial.distances[2]) / (stop - start) * i) * Mathf.Sin(j * offset * Mathf.PI / 180 * 360 / 365 / 24 / inc)),
                     -((sundial.distances[2] + (sundial.distances[3] - sundial.distances[2]) / (stop - start) * i) * Mathf.Cos(j * offset * Mathf.PI / 180 * 360 / 365 / 24 / inc))));
                i++;
            }
        }
        else if (straight.isOn == true && rotateTime.isOn == false)
        {
            int i = 0;
            for (int j = start; j < stop; j++)
            {
                lineRenderer.SetPosition(i, new Vector3(
                     earth.position.x + ((mars.position.x - earth.position.x) / (stop - start) * i) ,
                     ((float)j * 6 / 4800f / inc * (360f / 365f) * mainSlider.value),
                     earth.position.z + ((mars.position.z - earth.position.z) / (stop - start) * i)));
                i++;
            }
        }
        else if (straight.isOn == true && rotateTime.isOn == true)
        {
            int i = 0;
            for (int j = start; j < stop; j++)
            {
                lineRenderer.SetPosition(i, new Vector3(
                     ((float)j * 6 / 4800f / inc * (360f / 365f) * mainSlider.value),
                     earth.position.y - (mars.position.y - earth.position.y) / (stop - start) * i* offset,
                     0));
                i++;
            }
        }
    }
}
