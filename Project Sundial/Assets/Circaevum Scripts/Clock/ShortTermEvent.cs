using UnityEngine;
using UnityEngine.UI;

public class ShortTermEvent : MonoBehaviour
{
    private Sundial sundial;
    private LineRenderer lineRenderer;
    private GameObject clock;
    private GameObject ParentObject;
    private Toggle straight;
    private Toggle rotateTime;
    private Toggle zoomed;
    private float fromScale;
    private float fromWidth;
    private float toScale;
    private float toWidth;
    private float zoom_start = 0;
    private bool zooming = false;
    public bool flatDay = false;
    private Slider mainSlider;
    private Slider phase;
    public string body;
    public float radius;
    public float category;
    public int start;
    public int stop;
    private int julian;
    public float time;
    private float inc = 12;

    void Start()
    {
        julian = transform.parent.GetComponent<DayLocater>().julianDay;
        sundial = GameObject.Find("Sundial").GetComponent<Sundial>();
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = 0.005f;
        lineRenderer.endWidth = 0.005f;
        lineRenderer.useWorldSpace = false;
        ParentObject = GameObject.Find("Clock");
        mainSlider = GameObject.Find("Slider").GetComponent<Slider>();
        phase = GameObject.Find("PhaseShift").GetComponent<Slider>();
        straight = GameObject.Find("Straight Toggle").GetComponent<Toggle>();
        rotateTime = GameObject.Find("Toggle Rotate Time").GetComponent<Toggle>();
        zoomed = GameObject.Find("Zoom Toggle").GetComponent<Toggle>();
        zoomed.onValueChanged.AddListener(delegate { ZoomSystem(); });
        time = sundial.now;
        //Every single increment is 5 minutes. So if 0 = Jan 1, 2020, then 144 = 12pm
        //Seemed like a reasonable place to stop as far as resolution considerations
        //5 mins is a reasonable block to schedule down to.
        int i = 0;
        lineRenderer.positionCount = Mathf.Abs(stop - start);
        Flatten();
        float width = 0.01f*ParentObject.transform.parent.transform.localScale.x;
        lineRenderer.startWidth = width;
        lineRenderer.endWidth = width;
    }

    void Update()
    {
        if(Input.GetKeyDown("f"))
        {
            flatDay = !flatDay;
            if(!flatDay)
                Unflatten();
            else
                Flatten();
        }
        if (zoom_start!=0 && Time.time <= zoom_start + 1f)
        {
            float elapsedTime = (Time.time - zoom_start);
            if(zooming){
                float scale = Mathf.SmoothStep(fromScale, toScale, elapsedTime / 1f);
                float lineScale = Mathf.SmoothStep(fromWidth, toWidth, elapsedTime / 1f);
                transform.localScale = new Vector3(scale,scale,scale);
                lineRenderer.startWidth = lineScale;
                lineRenderer.endWidth = lineScale;
            }
        }
        else
        {
            zooming = false;
            zoom_start = 0;
        }
    }
    void ZoomSystem()
    {
        zooming = true;
        zoom_start = Time.time;
        float multiplier = 0.00625f;
        if (!zoomed.isOn)
        {
            fromScale = multiplier;
            fromWidth = 0.01f;
            toScale = 10*multiplier;
            toWidth = 0.01f;
        }
        else
        {
            fromScale = 10*multiplier;
            fromWidth = 0.001f;
            toScale = multiplier;
            toWidth = 0.1f;
        }
    }
    void Flatten()
    {
        int i = 0;
        for (int j = start; j < stop; j++)
        {
            lineRenderer.SetPosition(i, new Vector3(
                -(radius - 0.1f) * Mathf.Sin(Mathf.PI * (2 * j / 24f / inc)-(julian*phase.value)+180),
                0,//(j-time) / 240f / inc* mainSlider.value/4));
                (radius - 0.1f) * Mathf.Cos(Mathf.PI * (2 * j / 24f / inc)-(julian*phase.value)+180)));
            i++;
        }
    }
    
    void Unflatten()
    {
        int i = 0;
        for (int j = start; j < stop; j++)
        {
            lineRenderer.SetPosition(i, new Vector3(
                -(radius - 0.1f) * Mathf.Sin(Mathf.PI * (2 * j / 24f / inc)+180),
                j%288 / 240f / inc* mainSlider.value/4,
                (radius - 0.1f) * Mathf.Cos(Mathf.PI * (2 * j / 24f / inc)+180)));
            i++;
        }
    }
}
