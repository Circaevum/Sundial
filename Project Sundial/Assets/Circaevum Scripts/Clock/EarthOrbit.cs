    using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;

public class EarthOrbit : MonoBehaviour
{
    private float radius;
    private Sundial sundial;
    private LineRenderer lineRenderer;
    private GameObject ParentObject;
    public int planet;

    void Start()
    {
        sundial = GameObject.Find("Sundial").GetComponent<Sundial>();
        lineRenderer = GetComponent<LineRenderer>();
        ParentObject = GameObject.Find("Orbit");
        radius = ParentObject.GetComponent<ClockValues>().radius / 2;
        lineRenderer.startWidth = 0.02f;
        lineRenderer.endWidth = 0.02f;
        lineRenderer.positionCount = 361;
        for (int i = 0; i <= 360; i++)
        {
            lineRenderer.SetPosition(i, new Vector3(
                radius * sundial.distances[planet] / sundial.horizonScale * Mathf.Sin(i * Mathf.PI / 180),
                -radius * sundial.distances[planet] / sundial.horizonScale * Mathf.Cos(i * Mathf.PI / 180),
                0
                ));
        }
    }
    void Update(){
        float width = 0.01f*ParentObject.transform.parent.transform.localScale.x;
        lineRenderer.startWidth = width;
        lineRenderer.endWidth = width;
    }
}