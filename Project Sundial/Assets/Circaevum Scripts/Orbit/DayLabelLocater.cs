using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayLabelLocater : MonoBehaviour
{
    private Sundial sundial;
    private GameObject ParentObject;
    public int dayNum;
    private float radius;
    // Use this for initialization
    void Start()
    {
        ParentObject = GameObject.Find("Clock");
        radius = ParentObject.GetComponent<ClockValues>().radius / 1.875f;
        sundial = GameObject.Find("Sundial").GetComponent<Sundial>();
        transform.position = GameObject.Find("Orbit").transform.position + new Vector3(
               transform.parent.transform.localScale.x * radius *sundial.distances[2] / sundial.horizonScale * 0.92f * Mathf.Sin(dayNum * Mathf.PI / 180 * 360 / 365),
               0,
               transform.parent.transform.localScale.x * -radius*sundial.distances[2] / sundial.horizonScale * 0.92f * Mathf.Cos(dayNum * Mathf.PI / 180 * 360 / 365)
          );
        transform.localScale = transform.localScale.y * new Vector3(
            -transform.parent.transform.localScale.x,
            transform.parent.transform.localScale.y,
            transform.parent.transform.localScale.z
            );
    }

    void Update()
    {
        transform.LookAt(Camera.main.transform);
    }
    public void ClickedDay()
    {
        print("WE GOT THIS FAR");
        transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
    }

}
