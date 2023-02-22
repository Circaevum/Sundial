using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FlatDayMarkers: MonoBehaviour
{

    private LineRenderer lineRenderer;
    public float radius = 0.1f;
    private Sundial sundial;
    private float counter;
    public Transform cam;

    public float lineDrawSpeed;

    void Start()
    {
        sundial = GameObject.Find("Sundial").GetComponent<Sundial>();
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.useWorldSpace = false;
        for (int i = 0; i <= 1094; i += 3)
        {
            lineRenderer.SetPosition(i, new Vector3(0, 0, 0));
            lineRenderer.SetPosition(i + 1, new Vector3((sundial.distances[2] * Mathf.Sin(i / 3f * Mathf.PI / 180)), 0, -(sundial.distances[2] * Mathf.Cos(i / 3f * Mathf.PI / 180))));
            lineRenderer.SetPosition(i + 2, new Vector3(0, 0, 0));
        }
        lineRenderer.startWidth = 0.02f;
        lineRenderer.endWidth = 0.02f;
    }
    void Update()
    {
        for (int i = 0; i <= 1094; i += 3)
        {
            lineRenderer.SetPosition(i, new Vector3(0, cam.position.y, 0));
            lineRenderer.SetPosition(i + 1, new Vector3((sundial.distances[2] * Mathf.Sin(i / 3f * Mathf.PI / 180)), cam.position.y, -(sundial.distances[2] * Mathf.Cos(i / 3f * Mathf.PI / 180))));
            lineRenderer.SetPosition(i + 2, new Vector3(0, cam.position.y, 0));
        }
    }
}
