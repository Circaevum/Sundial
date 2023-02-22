using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InnerRing : MonoBehaviour {
    LineRenderer innerRing;
    public Transform Earth;
    private Sundial sundial;
    private Toggle rotateTime;
    private Toggle straight;
    public Transform cam;
    // Use this for initialization
    void Start () {
		innerRing = GetComponent<LineRenderer>();
        innerRing.positionCount =25;
        sundial = GameObject.Find("Sundial").GetComponent<Sundial>();
        rotateTime = GameObject.Find("Toggle Rotate Time").GetComponent<Toggle>();
        straight = GameObject.Find("Straight Toggle").GetComponent<Toggle>();
    }

    // Update is called once per frame
    void Update () {
        if (rotateTime.isOn == true && straight.isOn == false)
        {
            for (int i = 0; i < 25; i++)
            {
                innerRing.SetPosition(i, Earth.position + new Vector3(
                    0,
                    5f / sundial.horizonScale * Mathf.Sin((i * Mathf.PI / 12) + (cam.position.y * Mathf.PI / 180)),
                    -5f / sundial.horizonScale * Mathf.Cos((i * Mathf.PI / 12) + (cam.position.y * Mathf.PI / 180))
                    ));
            }
        }
        else if (rotateTime.isOn == true && straight.isOn == true)
        {
            for (int i = 0; i < 25; i++)
            {
                innerRing.SetPosition(i, Earth.position + new Vector3(
                    0,
                    5f / sundial.horizonScale * Mathf.Sin(i * Mathf.PI / 12),
                    -5f / sundial.horizonScale * Mathf.Cos(i * Mathf.PI / 12)
                    ));
            }
        }
        else if (rotateTime.isOn == false && straight.isOn == false)
        {
            for (int i = 0; i < 25; i++)
            {
                innerRing.SetPosition(i, Earth.position + new Vector3(
                    5f / sundial.horizonScale * Mathf.Sin((i * Mathf.PI / 12) + (cam.position.y * Mathf.PI / 180)),
                    0,
                    -5f / sundial.horizonScale * Mathf.Cos((i * Mathf.PI / 12) + (cam.position.y * Mathf.PI / 180))
                    ));
            }
        }
        else if (rotateTime.isOn == false && straight.isOn == true)
        {
            for (int i = 0; i < 25; i++)
            {
                innerRing.SetPosition(i, Earth.position + new Vector3(
                    5f / sundial.horizonScale * Mathf.Sin(i * Mathf.PI / 12),
                    0,
                    -5f / sundial.horizonScale * Mathf.Cos(i * Mathf.PI / 12)
                    ));
            }
        }
    }
}
