using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;

public class MarsMonthLines : MonoBehaviour {

    private LineRenderer lineRenderer;
    public Vector3[] coordinates;
    private float distance = 141.6f;
    public float radius = 0.1f;
    public float height = 10f;
    public Slider mainSlider;
    private Toggle straight;
    private Transform mars;
    private Toggle rotateTime;

    public float lineDrawSpeed;

    //public Transform[] eventArray = new Transform[3];
    // Use this for initialization
    void Start()
    {
        mainSlider = GameObject.Find("Slider").GetComponent<Slider>();
        mainSlider.onValueChanged.AddListener(delegate { Slider_Changed(mainSlider.value); });
        mars = GameObject.Find("Mars_gamma").GetComponent<Transform>();
        straight = GameObject.Find("Straight Toggle").GetComponent<Toggle>();
        straight.onValueChanged.AddListener(delegate { Start(); });
        rotateTime = GameObject.Find("Toggle Rotate Time").GetComponent<Toggle>();
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.useWorldSpace = false;
        for (int i = 0; i<= 72; i+=3)
        {
            lineRenderer.SetPosition(i,new Vector3(0, i * mainSlider.value / 3.333f, 0));
            lineRenderer.SetPosition(i+1,new Vector3((distance * Mathf.Sin(i*10f *365/687* Mathf.PI/180)), i * mainSlider.value / 3.333f, -(distance * Mathf.Cos(i*10f*365/687 * Mathf.PI/180))));
            lineRenderer.SetPosition(i+2,new Vector3(0, i * mainSlider.value / 3.333f, 0));
        }

        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
    }
    public void Slider_Changed(float newValue)
    {
        if (straight.isOn == false)
        {
            for (int i = 0; i <= 72; i += 3)
            {
                lineRenderer.SetPosition(i, new Vector3(0, i * mainSlider.value / 3.333f, 0));
                lineRenderer.SetPosition(i + 1, new Vector3((distance * Mathf.Sin(i * 10f * 365 / 687 * Mathf.PI / 180)), i * mainSlider.value / 3.333f, -(distance * Mathf.Cos(i * 10f * 365 / 687 * Mathf.PI / 180))));
                lineRenderer.SetPosition(i + 2, new Vector3(0, i * mainSlider.value / 3.333f, 0));
            }
        }
            
    }
    void Update()
    {
        if (straight.isOn == true && rotateTime.isOn == false)
        {
            for (int i = 0; i <= 72; i += 3)
            {
                lineRenderer.SetPosition(i, new Vector3(0, i * mainSlider.value / 3.333f, 0));
                lineRenderer.SetPosition(i + 1, new Vector3(mars.position.x * 0.95f, i * mainSlider.value / 3.333f, mars.position.z * 0.95f));
                lineRenderer.SetPosition(i + 2, new Vector3(0, i * mainSlider.value / 3.333f, 0));
            }
        }
        else if ((straight.isOn == true && rotateTime.isOn == true))
        {
            for (int i = 0; i <= 72; i += 3)
            {
                lineRenderer.SetPosition(i, new Vector3(0, i * mainSlider.value / 3.333f, 0));
                lineRenderer.SetPosition(i + 1, new Vector3(-mars.position.y * 0.95f, i * mainSlider.value / 3.333f, mars.position.z * 0.95f));
                lineRenderer.SetPosition(i + 2, new Vector3(0, i * mainSlider.value / 3.333f, 0));
            }
        }
    }
}
