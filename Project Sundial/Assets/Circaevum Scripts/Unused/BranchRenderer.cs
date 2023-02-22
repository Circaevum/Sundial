using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BranchRenderer : MonoBehaviour {

    private LineRenderer lineRenderer;
    public Vector3 start;
    public Vector3 stop;
    public float width = 0.2f;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.useWorldSpace = false;
        //lineRenderer.SetVertexCount(stop - start);
        lineRenderer.SetPosition(0, start + new Vector3(0, -50, 100));
        lineRenderer.SetPosition(1, stop + new Vector3(0, -50, 100));
        lineRenderer.startWidth = width;
        lineRenderer.endWidth = width;
    }

    void Update()
    {

    }
}
