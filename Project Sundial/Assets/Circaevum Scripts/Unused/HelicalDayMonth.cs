using UnityEngine;
using System.Collections;
using System.IO;

public class HelicalDayMonth : MonoBehaviour
{

    private LineRenderer lineRenderer;
    //private float counter;
    //private float dist;
    public float radius;
    public int month;
    private float check;

    private Sundial sundial;

    public float lineDrawSpeed;
    void Start()
    {
        sundial = GameObject.Find("Sundial").GetComponent<Sundial>();
        lineRenderer = GetComponent<LineRenderer>();
        for (int i = month*1095; i < (month+1)*1095; i++)
        {
            lineRenderer.SetPosition(i-month*1095, new Vector3(
                (sundial.distances[2] * Mathf.Sin(i*10 * Mathf.PI / 180 * 360 / 365 / 24 / 15)) + (radius * Mathf.Sin(2 * Mathf.PI * i * 10 / 24 / 15)),
                (i * radius/20 / 24 / 15* sundial.height),
                -(sundial.distances[2] * Mathf.Cos(i*10 * Mathf.PI / 180 * 360 / 365 / 24 / 15)) - (radius * Mathf.Cos(2 * Mathf.PI * i * 10 / 24 / 15))));
        }

         lineRenderer.SetWidth(0.1f, 0.08f);
        //dist = Vector3.Distance(origin.position, destination.position);
    }

    // Update is called once per frame
    void Update()
    {
       /* if(counter<dist)
        {
            counter += 0.1f / lineDrawSpeed;

            float x = Mathf.Lerp(0, dist, counter);

            Vector3 pointA = origin.position;
            Vector3 pointB = destination.position;

            Vector3 pointAlongLine = x * Vector3.Normalize(pointB - pointA) - pointA;

            lineRenderer.SetPosition(1, pointAlongLine);
        }
        */
    }
}
