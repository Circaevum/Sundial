using UnityEngine;
using UnityEngine.UI;

public class Worldline : MonoBehaviour
{
    LineRenderer worldline;
    private float radius;
    public float time;
    private int intervals;
    public int planet;
    private Sundial sundial;
    private GameObject ParentObject;
    private GameObject clock;
    private Slider mainSlider;
    void Start()
    {
        sundial = GameObject.Find("Sundial").GetComponent<Sundial>();
        ParentObject = GameObject.Find("Clock");
        intervals = 365;//(int)world.pDays[planet];
        radius = ParentObject.GetComponent<ClockValues>().radius/2;
        worldline = GetComponent<LineRenderer>();
        worldline.positionCount = intervals + 1;
        mainSlider = GameObject.Find("Slider").GetComponent<Slider>();
    }
    void Update()
    {
        time = sundial.now*360/288*360/sundial.pDays[planet];
        float angle = ParentObject.transform.parent.transform.eulerAngles.x;
        transform.eulerAngles = new Vector3(angle, 0,-time/360*(1-1/sundial.pDays[planet])*sundial.Vrot[planet]);
        for (int i = 0; i < intervals + 1; i++)
        {
            worldline.SetPosition(i, new Vector3(
                    -radius * sundial.distances[planet] / sundial.horizonScale * Mathf.Sin(i * Mathf.PI / 180f * 360f / sundial.pDays[planet]),
                    radius * sundial.distances[planet] / sundial.horizonScale * Mathf.Cos(i * Mathf.PI / 180f * 360f / sundial.pDays[planet]),
                    -i / 50f * sundial.Vrot[planet] * mainSlider.value / 12));
        }
        float width = 0.01f*ParentObject.transform.parent.transform.localScale.x;
        worldline.startWidth = width;
        worldline.endWidth = width;
    }
}
