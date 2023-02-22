using UnityEngine;
using UnityEngine.UI;
using System.Collections;

using System.IO;

public class EarthDayLineRenderer : MonoBehaviour {

    private LineRenderer lineRenderer;
    public float radius = 0.1f;
    private Sundial sundial;
    private float counter;
    public Slider mainSlider;
    private Toggle straight;
    private Transform earth;
    private Toggle rotateTime;

    public float lineDrawSpeed;

    //public Transform[] eventArray = new Transform[3];
    // Use this for initialization
    void Start()
    {
        mainSlider = GameObject.Find("Slider").GetComponent<Slider>();
        earth = GameObject.Find("Earth_16K_gamma").GetComponent<Transform>();
        straight = GameObject.Find("Straight Toggle").GetComponent<Toggle>();
        rotateTime = GameObject.Find("Toggle Rotate Time").GetComponent<Toggle>();
        sundial = GameObject.Find("Sundial").GetComponent<Sundial>();
    	lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.useWorldSpace = false;
        lineRenderer.startWidth = 0.02f;
        lineRenderer.endWidth = 0.02f;
    }
    void Update()
    {
        if (straight.isOn == false)
        {
            for (int i = 0; i <= 1094; i += 3)
            {
                lineRenderer.SetPosition(i, new Vector3(0, i * mainSlider.value / 100f, 0));
                lineRenderer.SetPosition(i + 1, new Vector3((sundial.distances[2]*0.9f * Mathf.Sin(i / 3f * Mathf.PI / 180)), i * mainSlider.value / 100f, -(sundial.distances[2] * 0.9f * Mathf.Cos(i / 3f * Mathf.PI / 180))));
                lineRenderer.SetPosition(i + 2, new Vector3(0, i * mainSlider.value / 100f, 0));
            }
        }
        else if (straight.isOn == true && rotateTime.isOn == false)
        {
            for (int i = 0; i <= 1094; i += 3)
            {
                lineRenderer.SetPosition(i, new Vector3(0, i * mainSlider.value / 100f, 0));
                lineRenderer.SetPosition(i + 1, new Vector3(earth.position.x * 0.9f, i * mainSlider.value / 100f, earth.position.z * 0.9f));
                lineRenderer.SetPosition(i + 2, new Vector3(0, i * mainSlider.value / 100f, 0));
            }
        }
        else if (straight.isOn == true && rotateTime.isOn == true)
        {
            for (int i = 0; i <= 1094; i += 3)
            {
                lineRenderer.SetPosition(i, new Vector3(0, i * mainSlider.value / 100f, 0));
                lineRenderer.SetPosition(i + 1, new Vector3(-earth.position.y * 0.9f, i * mainSlider.value / 100f, earth.position.z * 0.9f));
                lineRenderer.SetPosition(i + 2, new Vector3(0, i * mainSlider.value / 100f, 0));
            }
        }
    }
}
