using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;

public class MarsDayLineRenderer : MonoBehaviour {

    private LineRenderer lineRenderer;
    public Vector3[] coordinates;
    private float distance = 141.6f;
    public float radius = 0.1f;
    private float height = 0.3333f;
    private float twist = 3f;
    public Slider mainSlider;
    private Sundial sundial;
    private Toggle straight;
    private Transform mars;
    private Toggle rotateTime;

    public float lineDrawSpeed;

    //public Transform[] eventArray = new Transform[3];
    // Use this for initialization
    void Start()
    {
        sundial = GameObject.Find("Sundial").GetComponent<Sundial>();
        mainSlider = GameObject.Find("Slider").GetComponent<Slider>();
        mars = GameObject.Find("Mars_gamma").GetComponent<Transform>();
        straight = GameObject.Find("Straight Toggle").GetComponent<Toggle>();
        straight.onValueChanged.AddListener(delegate { Start(); });
        rotateTime = GameObject.Find("Toggle Rotate Time").GetComponent<Toggle>();
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.useWorldSpace = false;
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
    }
    void Update()
    {
        if (straight.isOn == false)
        {
            for (int i = 0; i <= 2091; i += 3)
            {
                lineRenderer.SetPosition(i, new Vector3(0, i * mainSlider.value / 100, 0));
                lineRenderer.SetPosition(i + 1, new Vector3((sundial.distances[3] * 0.95f * Mathf.Sin(i / twist * 365 / 687 * Mathf.PI / 180)), i * mainSlider.value / 100, -(sundial.distances[3] * 0.95f * Mathf.Cos(i / twist * 365 / 687 * Mathf.PI / 180))));
                lineRenderer.SetPosition(i + 2, new Vector3(0, i * mainSlider.value / 100, 0));
            }
        }
        else if (straight.isOn == true && rotateTime.isOn == false)
        {
            for (int i = 0; i <= 2091; i += 3)
            {
                lineRenderer.SetPosition(i, new Vector3(0, i * mainSlider.value / 100f, 0));
                lineRenderer.SetPosition(i + 1, new Vector3(mars.position.x * 0.95f, i * mainSlider.value / 100f, mars.position.z * 0.95f));
                lineRenderer.SetPosition(i + 2, new Vector3(0, i * mainSlider.value / 100f, 0));
            }
        }
        else if (straight.isOn == true && rotateTime.isOn == true)
        {
            for (int i = 0; i <= 2091; i += 3)
            {
                lineRenderer.SetPosition(i, new Vector3(0,  i * mainSlider.value / 100f, 0));
                lineRenderer.SetPosition(i + 1, new Vector3(-mars.position.y * 0.95f,  i * mainSlider.value / 100f, mars.position.z * 0.95f));
                lineRenderer.SetPosition(i + 2, new Vector3(0, i * mainSlider.value / 100f, 0));
            }
        }
    }
}
