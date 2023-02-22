using UnityEngine;
using UnityEngine.UI;

public class TimeFrame : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private Sundial sundial;
    private GameObject clock;
    private GameObject ParentObject;
    private Slider mainSlider;
    public string body;

    public float radius;
    public int start;
    public int stop;
    public int time;
    private float inc = 12;

    public float lineDrawSpeed;

    void Start()
    {
        sundial = GameObject.Find("Sundial").GetComponent<Sundial>();
        ParentObject = GameObject.Find("Clock");
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = 0.001f;
        lineRenderer.endWidth = 0.001f;
        mainSlider = GameObject.Find("Slider").GetComponent<Slider>();
        radius = ParentObject.GetComponent<ClockValues>().radius+0.25f;
    }

    void Update()
    {
        print(sundial.now.ToString()); 
        time = sundial.now;

        print("TEST222");
        start = time - 5*288;
        stop = time + 5 * 288;
        //Every single increment is 5 minutes. So if 0 = Jan 1, 2020, then 144 = 12pm
        //Seemed like a reasonable place to stop as far as resolution considerations
        //5 mins is a reasonable block to schedule down to.
        int i = 0;
        lineRenderer.positionCount = Mathf.Abs(stop - start);
        for (float j = start; j < stop; j++)
        {
            lineRenderer.SetPosition(i, new Vector3(
                (radius - 0.1f) * Mathf.Sin(Mathf.PI * (2 * j / 24f / inc) - Mathf.PI),
                (radius - 0.1f) * Mathf.Cos(Mathf.PI * (2 * j / 24f / inc) - Mathf.PI),
                (j - time ) / 240f / inc * mainSlider.value / 4));
            i++;
        }
        float width = 0.002f*ParentObject.transform.parent.transform.localScale.x;
        lineRenderer.startWidth = width;
        lineRenderer.endWidth = width;
    }
}
