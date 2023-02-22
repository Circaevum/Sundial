using UnityEngine;
using System.Collections;

using System.IO;

public class DrawChartAxes : MonoBehaviour
{

    private LineRenderer lineRenderer;
    public Vector3[] coordinates;
    public float radius = 0.1f;

    
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.useWorldSpace = false;
        lineRenderer.SetPosition(0, new Vector3(0,15,0));
        lineRenderer.SetPosition(1, new Vector3(0, 0, 0));
        lineRenderer.SetPosition(2, new Vector3(0, 0, 15));
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
    }
    void Update()
    {
        /*
        counter += 0.001f;
        if (counter <= sundial.height/33.333f)
        {
            for (int i = 0; i <= 36; i += 3)
            {
                lineRenderer.SetPosition(i, new Vector3(0, i * sundial.height / 3.333f * counter, 0));
                lineRenderer.SetPosition(i + 1, new Vector3((sundial.distances[2] * Mathf.Sin(i * 10 * Mathf.PI / 180)), i * sundial.height / 3.333f * counter, -(sundial.distances[2] * Mathf.Cos(i * 10 * Mathf.PI / 180))));
                lineRenderer.SetPosition(i + 2, new Vector3(0, i * sundial.height / 3.333f * counter, 0));
            }
        }
        */
    }
}
