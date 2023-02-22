using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;

public class MoonPath : MonoBehaviour
{

    private LineRenderer lineRenderer;
    public Slider mainSlider;
    private Toggle straight;
    private float counter;
    private float dist;
    public float radius;
    public int month;
    private float check;
    private Transform origin;
    private Transform earth;
    private Toggle rotateTime;
    private float moonX = 10f;

    private Sundial sundial;

    public float lineDrawSpeed;
    void Start()
    {
        origin = GameObject.Find("Y-Origin").GetComponent<Transform>();
        earth = GameObject.Find("Earth_16K_gamma").GetComponent<Transform>();
        straight = GameObject.Find("Straight Toggle").GetComponent<Toggle>();
        straight.onValueChanged.AddListener(delegate { Start(); });
        mainSlider = GameObject.Find("Slider").GetComponent<Slider>();
        rotateTime = GameObject.Find("Toggle Rotate Time").GetComponent<Toggle>();

        sundial = GameObject.Find("Sundial").GetComponent<Sundial>();
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.useWorldSpace = false;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        //dist = Vector3.Distance(origin.position, destination.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (straight.isOn == true && rotateTime.isOn == false)
        {
            for (int i = month * 360; i < (month + 1) * 360; i++)
            {
                lineRenderer.SetPosition(i - month * 360, new Vector3(
                    earth.position.x + moonX * Mathf.Sin((i + 130f / 24f) * 11f * Mathf.PI / 180f), 
                    i * mainSlider.value / 33.333f,
                    earth.position.z - moonX * Mathf.Cos((i + 130f / 24f) * 11f * Mathf.PI / 180f)));
            }
        }
        else if (straight.isOn == false)
        {
            for (int i = month * 360; i < (month + 1) * 360; i++)
            {
                lineRenderer.SetPosition(i - month * 360, new Vector3(
                    (sundial.distances[2] * Mathf.Sin(i * Mathf.PI / 180f)) + moonX * Mathf.Sin((i + 130f / 24f) * 11f * Mathf.PI / 180f),
                    i * mainSlider.value / 33.333f,
                    -(sundial.distances[2] * Mathf.Cos(i * Mathf.PI / 180f)) - moonX * Mathf.Cos((i + 130f / 24f) * 11f * Mathf.PI / 180f)));
            }
        }
        else if (straight.isOn == true && rotateTime.isOn == true)
        {
            for (int i = month * 360; i < (month + 1) * 360; i++)
            {
                lineRenderer.SetPosition(i - month * 360, new Vector3(
                    -earth.position.y - moonX * Mathf.Sin((i + 130f / 24f) * 11f * Mathf.PI / 180f),
                    i * mainSlider.value / 33.333f,
                    earth.position.z - moonX * Mathf.Cos((i + 130f / 24f) * 11f * Mathf.PI / 180f)));
            }
        }
    }
}
