using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;

public class OrbitalRing : MonoBehaviour {

    private LineRenderer lineRenderer;
    //GameObject  = GameObject.Find ("MyObject");
    private Sundial sundial;
    public float worldHeight;
    public Slider mainSlider;
    private Toggle rotateTime;

    //public float[] distance;
    // = public GameObject.Find ("Parameters");
    //public spacetime number = sundial.GetComponent <spacetime>();
    //public float[] realdistance = new float[];
    //private float[] distance = new float[] {36f,67.24f,93f,141.6f,483.8f,888.2f,1787f,2795f,3670f};

    public int planet;

    public Transform cam;

    public float lineDrawSpeed;

    void Start()
    {
        mainSlider = GameObject.Find("Slider").GetComponent<Slider>();
        rotateTime = GameObject.Find("Toggle Rotate Time").GetComponent<Toggle>();
        sundial = GameObject.Find("Sundial").GetComponent<Sundial>();
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.useWorldSpace = false;

        lineRenderer.startWidth = 0.02f;
        lineRenderer.endWidth = 0.02f;
    }
    void Update()
    {
        for (int i = 0; i <= 360; i++)
        {
            if (rotateTime.isOn == true)
            {
                lineRenderer.SetPosition(i, new Vector3(
                    sundial.distances[planet] / sundial.horizonScale * Mathf.Sin(i * Mathf.PI / 180),
                    cam.position.x / 1.5f * mainSlider.value / 33.333f/2 * 365 / 360,
                    -sundial.distances[planet] / sundial.horizonScale * Mathf.Cos(i * Mathf.PI / 180)));
            }
            else if (rotateTime.isOn == false)
            {
                lineRenderer.SetPosition(i, new Vector3(
                    sundial.distances[planet] / sundial.horizonScale * Mathf.Sin(i * Mathf.PI / 180),
                    cam.position.y / 1.5f * mainSlider.value / 33.333f/2 * 365 / 360,
                    -sundial.distances[planet] / sundial.horizonScale * Mathf.Cos(i * Mathf.PI / 180)));
            }

        }
    }
}