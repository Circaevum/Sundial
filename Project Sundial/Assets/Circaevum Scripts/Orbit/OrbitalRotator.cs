using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrbitalRotator : MonoBehaviour
{
	private Sundial sundial;
    private GameObject clock;
    public float time;
    public float start=0;
    private Toggle rotated;
    private Vector3 position= new Vector3(0,0,0);
    private Vector3 fromPosition;
    private Vector3 toPosition;
    private Toggle zoomed;
    private Boolean rotating = false;
    private Boolean zooming = false;

    private void Start()
    {
        sundial = GameObject.Find("Sundial").GetComponent<Sundial>();
        zoomed = GameObject.Find("Zoom Toggle").GetComponent<Toggle>();
        rotated = GameObject.Find("Toggle Rotate Time").GetComponent<Toggle>();
        rotated.onValueChanged.AddListener(delegate { RotateSystem(); });
        zoomed.onValueChanged.AddListener(delegate { ZoomSystem(); });
    }
    void RotateSystem()
    {
        rotating = true;
        start = Time.time;
        //We only need to reposition the Orbit origin if we are rotating while zoomed in (since Earth is centerpoint)
        if (zoomed.isOn)
        {
            if (rotated.isOn)
            {
                fromPosition = new Vector3(
                    -sundial.distances[2]  * Mathf.Sin(time * Mathf.PI / 180f/ 288f), 
                    0, 
                    sundial.distances[2]  * Mathf.Cos(time * Mathf.PI / 180f / 288f)
                );
                toPosition = new Vector3(
                    -sundial.distances[2]  * Mathf.Sin(time * Mathf.PI / 180f  / 288f), 
                    -sundial.distances[2] * Mathf.Cos(time * Mathf.PI / 180f / 288f),
                    0
                );
            }
            else
            {
                fromPosition = new Vector3(
                    -sundial.distances[2]  * Mathf.Sin(time * Mathf.PI / 180f  / 288f), 
                    -sundial.distances[2] * Mathf.Cos(time * Mathf.PI / 180f / 288f),
                    0
                );
                toPosition = new Vector3(
                    -sundial.distances[2]  * Mathf.Sin(time * Mathf.PI / 180f/ 288f), 
                    0, 
                    sundial.distances[2]  * Mathf.Cos(time * Mathf.PI / 180f / 288f)
                );
            }
        }
        else{
            fromPosition = new Vector3(0,0,0);
            toPosition = new Vector3(0,0,0);
        }
    }
    void ZoomSystem()
    {
        zooming = true;
        start = Time.time;
        //When zooming out, we need to establish starting and ending positions for the Orbital origin (where the Sun is)
        //Ending position will be back at 0,0,0
        if (!zoomed.isOn)
        {
            if (!rotated.isOn)
            {
                fromPosition = new Vector3(
                    -sundial.distances[2]  * Mathf.Sin(time * Mathf.PI / 180f/ 288f), 
                    0, 
                    sundial.distances[2]  * Mathf.Cos(time * Mathf.PI / 180f / 288f)
                );
            }
            else
            {
                fromPosition = transform.position = new Vector3(
                    -sundial.distances[2]  * Mathf.Sin(time * Mathf.PI / 180f  / 288f), 
                    -sundial.distances[2] * Mathf.Cos(time * Mathf.PI / 180f / 288f),
                    0
                );
            }
            toPosition = new Vector3(0,0,0);
        }
        //When zooming in, we need to establish that the start of the Orbital origin moves from 0,0,0 to the inverse of the Earth's respective position,
        //This way the Earth can end up at 0,0,0
        else
        {
            fromPosition = new Vector3(0,0,0);
            if (!rotated.isOn)
            {
                toPosition = new Vector3(
                    -sundial.distances[2]  * Mathf.Sin(time * Mathf.PI / 180f/ 288f), 
                    0, 
                    sundial.distances[2]  * Mathf.Cos(time * Mathf.PI / 180f / 288f)
                );
            }
            else
            {
                toPosition = transform.position = new Vector3(
                    -sundial.distances[2]  * Mathf.Sin(time * Mathf.PI / 180f  / 288f), 
                    -sundial.distances[2] * Mathf.Cos(time * Mathf.PI / 180f / 288f),
                    0
                );
            }
        }
    }
    private void Update()
    {
        time = sundial.now*360/sundial.pDays[2];
        
        //If the rotating or zooming functions were activated within 4 seconds, then do the positional transfers
        if (start!=0 && Time.time <= start + 1f)
        {
            float elapsedTime = (Time.time - start);
            if(zooming||rotating){
                position = new Vector3(
                    Mathf.SmoothStep(fromPosition.x, toPosition.x, elapsedTime ),
                    Mathf.SmoothStep(fromPosition.y, toPosition.y, elapsedTime ),
                    Mathf.SmoothStep(fromPosition.z, toPosition.z, elapsedTime )
                );
                transform.position = position;
            }
        }
        //Otherwise just follow the regular behaviors of the Orbital system movement, like rotating based off of the current time values
        else
        {
            zooming = false;
            start = 0;
            if(zoomed.isOn)
            {
                if (!rotated.isOn)
                    transform.position = new Vector3(
                        -sundial.distances[2]  * Mathf.Sin(time * Mathf.PI / 180f/ 288f), 
                        0, 
                        sundial.distances[2]  * Mathf.Cos(time * Mathf.PI / 180f / 288f)
                    );
                else
                    transform.position = new Vector3(
                        -sundial.distances[2]  * Mathf.Sin(time * Mathf.PI / 180f  / 288f), 
                        -sundial.distances[2] * Mathf.Cos(time * Mathf.PI / 180f / 288f),
                        0
                    );
            }
            else
            {
                transform.position = new Vector3(0,0,0);
            }
        }
    }
}
