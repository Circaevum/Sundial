using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockRing : MonoBehaviour
{
    LineRenderer clockRing;
    public float radius;
    void Start()
    {
        radius = gameObject.transform.parent.gameObject.GetComponent<ClockValues>().radius;
        clockRing = GetComponent<LineRenderer>();
        clockRing.positionCount = 50;
        for (int i = 0; i < 50; i++)
        {
            if (i < 25)
            {
                clockRing.SetPosition(i, new Vector3(
                    radius * Mathf.Sin(i * Mathf.PI / 12),
                    -radius * Mathf.Cos(i * Mathf.PI / 12),
                    0
                    ));
            }
            else
            {
                clockRing.SetPosition(i, new Vector3(
                (radius + 0.1f) * Mathf.Sin((i-1) * Mathf.PI / 12),
                -(radius + 0.1f) * Mathf.Cos((i - 1) * Mathf.PI / 12),
                0
                ));
            }
        }
    }
}
