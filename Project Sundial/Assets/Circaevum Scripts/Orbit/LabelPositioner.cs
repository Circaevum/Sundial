using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LabelPositioner : MonoBehaviour
{
    private Sundial sundial;
    public Transform TextObject;
    public GameObject ParentObject;
    public int monthNum;
    private float radius;
    // Use this for initialization
    void Start()
    {
        radius = ParentObject.GetComponent<ClockValues>().radius / 2;
        sundial = GameObject.Find("Sundial").GetComponent<Sundial>();
        transform.position = GameObject.Find("Orbit").transform.position + new Vector3(
            radius * ParentObject.transform.localScale.x * (sundial.distances[2] / sundial.horizonScale) * 0.75f * Mathf.Sin((monthNum + 0.5f) * 30 * Mathf.PI / 180) * (360f / 365f),
            0,
            -radius * ParentObject.transform.localScale.y * (sundial.distances[2] / sundial.horizonScale) * 0.75f * Mathf.Cos((monthNum + 0.5f) * 30 * Mathf.PI / 180) * (360f / 365f)
            );
    }
    void Update()
    {
        TextObject.LookAt(Camera.main.transform);
    }

}
