using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;

public class MarsYearHelix : MonoBehaviour
{

    private LineRenderer lineRenderer;
    private float counter;
    private float dist;
    private Sundial sundial;
    public Vector3[] coordinates;
    public float radius = 6f;
    public Slider mainSlider;


    public float lineDrawSpeed;
    void Start()
    {
        mainSlider = GameObject.Find("Slider").GetComponent<Slider>();
        mainSlider.onValueChanged.AddListener(delegate { Slider_Changed(mainSlider.value); });
        sundial = GameObject.Find("Sundial").GetComponent<Sundial>();
        lineRenderer = GetComponent<LineRenderer>();
        for (int i = 0; i < 687; i++)
        {
            lineRenderer.SetPosition(i, new Vector3(
                (sundial.distances[3] * Mathf.Sin(i  * Mathf.PI / 180 * 365f / 687)),
                (i *3* mainSlider.value/100 ),// 2/3
                -(sundial.distances[3] * Mathf.Cos(i  * Mathf.PI / 180 * 365f / 687))));
        }

        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        //dist = Vector3.Distance(origin.position, destination.position);
    }
    public void Slider_Changed(float newValue)
    {
        for (int i = 0; i < 687; i++)
        {
            lineRenderer.SetPosition(i, new Vector3(
                (sundial.distances[3] * Mathf.Sin(i * Mathf.PI / 180 * 365f / 687)),
                (i *3* mainSlider.value/100 ),// 2/3
                -(sundial.distances[3] * Mathf.Cos(i * Mathf.PI / 180 * 365f / 687))));
        }
    }
    // Update is called once per frame
    void Update()
    {
    }
}
