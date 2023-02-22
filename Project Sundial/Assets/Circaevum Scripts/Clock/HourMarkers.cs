using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HourMarkers : MonoBehaviour
{
    LineRenderer hourMarkers;
    public float radius;
    public float width;
    void Start()
    {
        radius = gameObject.transform.parent.gameObject.GetComponent<ClockValues>().radius+width;
        hourMarkers = GetComponent<LineRenderer>();
        hourMarkers.positionCount = 75;
        for (int i = 0; i < 75; i++)
        {
            if (i % 3 == 0)
            {
                hourMarkers.SetPosition(i, new Vector3(
                  (radius-width) * Mathf.Sin(i * Mathf.PI / 36),
                  -(radius - width) * Mathf.Cos(i * Mathf.PI / 36),
                  0
                ));
            }
            else
            {
                hourMarkers.SetPosition(i, new Vector3(
                  radius * Mathf.Sin(i * Mathf.PI / 36),
                  -radius * Mathf.Cos(i * Mathf.PI / 36),
                  0
               ));
            }

        }
    }

}
