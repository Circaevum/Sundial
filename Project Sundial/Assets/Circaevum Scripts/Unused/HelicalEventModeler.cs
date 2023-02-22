using UnityEngine;
using System.Collections;
using System.IO;

public class HelicalEventModeler : MonoBehaviour
{

    private LineRenderer lineRenderer;
    private float counter;
    private float dist;
    

    private Sundial sundial;

    //public Transform origin;
    //public Transform destination;

    public float radius = 0.1f;
    public int start;
    public int stop;
    private float inc = 12;
    private float checkHeight;

    public float lineDrawSpeed;

    //public Transform[] eventArray = new Transform[3];
    // Use this for initialization
    void Start()
    {
        sundial = GameObject.Find("Sundial").GetComponent<Sundial>();
    	int i=0;
    	lineRenderer = GetComponent<LineRenderer>();
    	lineRenderer.SetVertexCount(stop-start);
        for (int j = start; j < stop; j++)
        {
            lineRenderer.SetPosition(i, new Vector3(
                (sundial.distances[2] * Mathf.Sin((float)j * Mathf.PI / 180 * 360f / 365f / 24f / inc )) ,
                ((float)j * radius/20f/10f / 24f / inc*(360f / 365f)* sundial.height),
                -(sundial.distances[2] * Mathf.Cos((float)j * Mathf.PI / 180 * 360f / 365f / 24f / inc ))));
            i++;
            Debug.Log(i);
        }

        lineRenderer.SetWidth(0.2f, 0.2f);
        checkHeight = sundial.height;
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
