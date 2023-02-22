using UnityEngine;
using UnityEngine.UI;

public class OrbitEvent : MonoBehaviour
{
    private Sundial sundial;
    private LineRenderer lineRenderer;
    private Slider mainSlider;
    private GameObject clock;
    private GameObject ParentObject;
    public string body;
    public float radius;
    public int start;
    public int stop;
    public float time;
    public int planet;
    private float inc = 12;

    public float lineDrawSpeed;

    //public Transform[] eventArray = new Transform[3];
    // Use this for initialization
    void Start()
    {
        sundial = GameObject.Find("Sundial").GetComponent<Sundial>();
        ParentObject = GameObject.Find("Clock");
        mainSlider = GameObject.Find("Slider").GetComponent<Slider>();
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.useWorldSpace = false;
        lineRenderer.startWidth = 0.01f;
        lineRenderer.endWidth = 0.01f;
        radius = ParentObject.GetComponent<ClockValues>().radius/2;

    }
    // Update is called once per frame

    void Update()
    {
        time = sundial.now;
        //Every single increment is 5 minutes. So if 0 = Jan 1, 2020, then 144 = 12pm
        //Seemed like a reasonable place to stop as far as resolution considerations
        //5 mins is a reasonable block to schedule down to.
        int i = 0;
        if (Mathf.Abs((start - time * 288) / 240f / inc * mainSlider.value / 4) <= 10000000)
        {
            lineRenderer.positionCount = Mathf.Abs(stop - start);
            for (int j = start; j < stop; j++)
            {
                lineRenderer.SetPosition(i, new Vector3(
                    radius * sundial.distances[planet] / sundial.horizonScale * Mathf.Sin(j /288f* Mathf.PI / 180),
                    radius * sundial.distances[planet] / sundial.horizonScale * Mathf.Cos(j / 288f * Mathf.PI / 180),
                    0.05f));
                i++;
            }
        }
        else
        {
            lineRenderer.positionCount =0;
        }
    }
}
