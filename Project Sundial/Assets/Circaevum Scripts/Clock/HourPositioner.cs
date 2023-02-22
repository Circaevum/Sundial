using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HourPositioner : MonoBehaviour
{
    TextMeshPro hour;
    private Sundial sundial;
    private GameObject ParentObject;
    public int hourNum;
    private float radius;
    // Use this for initialization
    void Start()
    {
        ParentObject = GameObject.Find("Clock");
        radius = ParentObject.GetComponent<ClockValues>().radius+0.075f;
        sundial = GameObject.Find("Sundial").GetComponent<Sundial>();
        transform.position = GameObject.Find("Hours").GetComponent<Transform>().position+ new Vector3(
            -radius * Mathf.Sin(hourNum * Mathf.PI / 12),
            -radius * Mathf.Cos(hourNum * Mathf.PI / 12),
            0
            );
        hour = GetComponent<TextMeshPro>();
        hour.text = hourNum.ToString();
    }
    void Update()
    {
        //hour.transform.rotation = Camera.main.transform.rotation;
    }
}
