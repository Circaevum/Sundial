using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FlatMonthMarkers : MonoBehaviour
{
    private Sundial sundial;
    private LineRenderer lineRenderer;
    private GameObject ParentObject;
    private float radius;

    // Use this for initialization
    void Start()
    {
        sundial = GameObject.Find("Sundial").GetComponent<Sundial>();
        ParentObject = GameObject.Find("Clock");
        radius = ParentObject.GetComponent<ClockValues>().radius/2;
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.useWorldSpace = false;
        int totalDays = 0;
        for (int i = 0; i <= 36; i += 3)
        {
            lineRenderer.SetPosition(i, new Vector3(0, 0, 0));
            lineRenderer.SetPosition(i + 1, new Vector3(
                -radius*sundial.distances[2] / sundial.horizonScale * Mathf.Sin(totalDays*360/365 * Mathf.PI / 180),
                -radius * (sundial.distances[2] / sundial.horizonScale * Mathf.Cos(totalDays * 360 / 365 * Mathf.PI / 180)),
                0));
            lineRenderer.SetPosition(i + 2, new Vector3(0, 0, 0));
            totalDays += sundial.monthLength[i / 3];
        }
        lineRenderer.SetWidth(0.02f, 0.02f);
    }
}
