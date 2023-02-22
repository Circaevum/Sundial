using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;

public class EarthGimbal : MonoBehaviour
{

    private LineRenderer lineRenderer;
    public Transform cam;
    public Slider mainSlider;
    public float worldHeight;

    private Sundial sundial;
    //public Transform origin;
    //public Transform destination;

    public Vector3[] coordinates;
    private float radius = 6f;

    public float lineDrawSpeed;

    //public Transform[] eventArray = new Transform[3];
    // Use this for initialization
    void Start()
    {
        mainSlider = GameObject.Find("Slider").GetComponent<Slider>();
        sundial = GameObject.Find("Sundial").GetComponent<Sundial>();
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, new Vector3(0,0,-93));
        lineRenderer.SetPosition(1, new Vector3(5,0,-93));


        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        //dist = Vector3.Distance(origin.position, destination.position);
    }
        // Update is called once per frame
    void Update()
    {
        sundial = GameObject.Find("Sundial").GetComponent<Sundial>();
        lineRenderer.SetPosition(0, new Vector3(
            (sundial.distances[2] * Mathf.Sin((cam.position.y) * Mathf.PI / 180f)),
            cam.position.y* mainSlider.value / 33.333f,
         - (sundial.distances[2] * Mathf.Cos((cam.position.y) * Mathf.PI / 180f))));
    	lineRenderer.SetPosition(1, new Vector3(
            (sundial.distances[2] * Mathf.Sin((cam.position.y) * Mathf.PI / 180f)) - (radius * Mathf.Sin(-cam.position.y *365f * Mathf.PI / 180f)),
            cam.position.y* mainSlider.value / 33.333f,
        - (sundial.distances[2] * Mathf.Cos((cam.position.y) * Mathf.PI / 180f)) - (radius * Mathf.Cos(-cam.position.y *365f * Mathf.PI / 180f))));

    }
}
