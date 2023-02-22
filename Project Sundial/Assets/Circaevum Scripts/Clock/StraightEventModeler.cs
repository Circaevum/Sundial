using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;

public class StraightEventModeler : MonoBehaviour
{
    public GameObject eventText;

    private LineRenderer lineRenderer;
    private float counter;
    private float dist;
    private Slider mainSlider;
    private Toggle straight;
    private Transform earth;
    private Transform origin;
    private Sundial sundial;
    private Toggle rotateTime;


    public float radius;
    public int start;
    public int stop;
    private float inc = 12;
    private float checkHeight;

    public float lineDrawSpeed;


    void Start()
    {
        earth = GameObject.Find("Earth_16K_gamma").GetComponent<Transform>();
        origin = GameObject.Find("Y-Origin").GetComponent<Transform>();
        straight = GameObject.Find("Straight Toggle").GetComponent<Toggle>();
        straight.onValueChanged.AddListener(delegate { Start(); });
        mainSlider = GameObject.Find("Slider").GetComponent<Slider>();
        rotateTime = GameObject.Find("Toggle Rotate Time").GetComponent<Toggle>();
        mainSlider.onValueChanged.AddListener(delegate { Slider_Changed(mainSlider.value); });
        sundial = GameObject.Find("Sundial").GetComponent<Sundial>();
        int i = 0;
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.useWorldSpace = false;
        lineRenderer.SetVertexCount(stop - start);
        int k = ((start+8640) + (stop - start) / 2);
        if (straight.isOn == true)
        {
            for (int j = start + 8640; j < stop + 8640; j++)
            {
                lineRenderer.SetPosition(i, new Vector3(
                     earth.position.x + ((radius - 0.1f) * Mathf.Sin(Mathf.PI * (2 * j / 24f / inc))),
                     ((float)j * radius / 4800f / inc * (360f / 365f) * mainSlider.value),
                     earth.position.z - ((radius - 0.1f) * Mathf.Cos(Mathf.PI * (2 * j / 24f / inc)))));
                i++;
            }
        }
        else if (straight.isOn == false)
        {
            for (int j = start + 8640; j < stop + 8640; j++)
            {
                lineRenderer.SetPosition(i, new Vector3(
                     sundial.distances[2] * Mathf.Sin(j * Mathf.PI / 180 * 360 / 365 / 24 / inc) + ((radius - 0.1f) * Mathf.Sin(Mathf.PI * (2 * j / 24f / inc))),
                     ((float)j * radius / 4800f / inc * (360f / 365f) * mainSlider.value),
                     -sundial.distances[2] * Mathf.Cos(j * Mathf.PI / 180 * 360 / 365 / 24 / inc) - ((radius - 0.1f) * Mathf.Cos(Mathf.PI * (2 * j / 24f / inc)))));
                i++;
            }
        }
            lineRenderer.startWidth = 0.2f;
        lineRenderer.endWidth = 0.2f;
        checkHeight = mainSlider.value;
        
    }
    public void Slider_Changed(float newValue)
    {
        Debug.Log(newValue);
        int i = 0;
        int k = ((start + 8640) + (stop - start) / 2);
        if (straight.isOn == false)
        {
            for (int j = start + 8640; j < stop + 8640; j++)
            {
                lineRenderer.SetPosition(i, new Vector3(
                     sundial.distances[2] * Mathf.Sin(j * Mathf.PI / 180 * 360 / 365 / 24 / inc) + ((radius - 0.1f) * Mathf.Sin(Mathf.PI * (2 * j / 24f / inc))),
                     ((float)j * radius / 4800f / inc * (360f / 365f) * mainSlider.value),
                     -sundial.distances[2] * Mathf.Cos(j * Mathf.PI / 180 * 360 / 365 / 24 / inc) - ((radius - 0.1f) * Mathf.Cos(Mathf.PI * (2 * j / 24f / inc)))));
                i++;
            }
        }
    }

    void Update()
    {
        int i = 0;
        int k = ((start + 8640) + (stop - start) / 2);
        if (straight.isOn == true && rotateTime.isOn == false)
        {
            for (int j = start + 8640; j < stop + 8640; j++)
            {
                lineRenderer.SetPosition(i, new Vector3(
                     earth.position.x + ((radius - 0.1f) * Mathf.Sin(Mathf.PI * (2 * j / 24f / inc) + origin.position.y * Mathf.PI / 180)),
                     ((float)j * radius / 4800f / inc * (360f / 365f) * mainSlider.value),
                     earth.position.z - ((radius - 0.1f) * Mathf.Cos(Mathf.PI * (2 * j / 24f / inc) + origin.position.y * Mathf.PI / 180))));
                i++;
            }
        } else if (straight.isOn == true && rotateTime.isOn == true)
        {
            for (int j = start + 8640; j < stop + 8640; j++)
            {
                lineRenderer.SetPosition(i, new Vector3(
                     -earth.position.y + ((radius - 0.1f) * Mathf.Sin(Mathf.PI * (2 * j / 24f / inc) + origin.position.x * Mathf.PI / 180)),
                     ((float)j * radius / 4800f / inc * (360f / 365f) * mainSlider.value),
                     earth.position.z - ((radius - 0.1f) * Mathf.Cos(Mathf.PI * (2 * j / 24f / inc) + origin.position.x * Mathf.PI / 180))));
                i++;
            }
        }

    }
}
