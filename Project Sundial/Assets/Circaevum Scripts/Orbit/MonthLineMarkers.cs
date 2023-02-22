using UnityEngine;
using System.Collections;
using UnityEngine.UI;

using System.IO;

public class MonthLineMarkers : MonoBehaviour {

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
        earth = GameObject.Find("Earth_8K").GetComponent<Transform>();
        straight = GameObject.Find("Straight Toggle").GetComponent<Toggle>();
        straight.onValueChanged.AddListener(delegate { Start(); });
        rotateTime = GameObject.Find("Toggle Rotate Time").GetComponent<Toggle>();
        sundial = GameObject.Find("Sundial").GetComponent<Sundial>();
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.useWorldSpace = false;
        lineRenderer.startWidth = 0.02f;
        lineRenderer.endWidth = 0.02f;
    }
    public void Update()
    {
        if (straight.isOn == true && rotateTime.isOn == false)
        {
            for (int i = 0; i <= 36; i += 3)
            {
                lineRenderer.SetPosition(i, new Vector3(0, i / 10f * mainSlider.value , 0));
                lineRenderer.SetPosition(i + 1, new Vector3(
                    0,
                    i/10f * mainSlider.value ,
                    -sundial.distances[2] / sundial.horizonScale 
                ));
                lineRenderer.SetPosition(i + 2, new Vector3(0, i / 10f * mainSlider.value , 0));
            }
        }
        else if (straight.isOn == true && rotateTime.isOn == true)
        {
            for (int i = 0; i <= 36; i += 3)
            {
                lineRenderer.SetPosition(i, new Vector3(0, i / 10f * mainSlider.value , 0));
                lineRenderer.SetPosition(i + 1, new Vector3(
                    sundial.distances[2] / sundial.horizonScale,
                    i / 10f * mainSlider.value ,
                    0
                ));
                lineRenderer.SetPosition(i + 2, new Vector3(0, i / 10f * mainSlider.value , 0));
            }
        }
        else
        {
            for (int i = 0; i <= 36; i += 3)
            {
                lineRenderer.SetPosition(i, new Vector3(0, i / 10f * mainSlider.value , 0));
                lineRenderer.SetPosition(i + 1, new Vector3(
                    (sundial.distances[2] / sundial.horizonScale * Mathf.Sin(i * 10f * Mathf.PI / 180)),
                    i/ 10f * mainSlider.value ,
                    -(sundial.distances[2] / sundial.horizonScale * Mathf.Cos(i * 10f * Mathf.PI / 180))
                ));
                lineRenderer.SetPosition(i + 2, new Vector3(0, i / 10f * mainSlider.value , 0));
            }
        }    
            
    }
}
