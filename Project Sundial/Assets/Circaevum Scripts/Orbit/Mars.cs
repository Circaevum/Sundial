using UnityEngine;
using System.Collections;
using System.IO;

public class Mars : MonoBehaviour
{

    private LineRenderer lineRenderer;
    private float counter;
    private float dist;
    private Sundial sundial;

    //public Transform origin;
    //public Transform destination;

    public Vector3[] coordinates;
    public float distance = 93f;
    public float radius = 2f;
    public float trip = 141.6f;

    public float lineDrawSpeed;

    //public Transform[] eventArray = new Transform[3];
    // Use this for initialization
    void Start()
    {
    	sundial = GameObject.Find("Sundial").GetComponent<Sundial>();
        lineRenderer = GetComponent<LineRenderer>();
        for (int i = 0; i < 6070; i++)
        {
        	distance+=(trip-93f)/6070f*5f;
            lineRenderer.SetPosition(i, new Vector3(
                (distance * Mathf.Sin(i*3 * Mathf.PI / 180 * 360 / 365 / 24 )) ,
                (i * radius/20 / 24 *sundial.height),
                -(distance * Mathf.Cos(i*3 * Mathf.PI / 180 * 360 / 365 / 24 )) ));
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
