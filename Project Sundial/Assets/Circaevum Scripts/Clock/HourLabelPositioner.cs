using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HourLabelPositioner : MonoBehaviour
{
    public Transform TextObject;
  public float offset;
    private Sundial sundial;
    private Toggle straight;
    private Transform earth;
    private Transform origin;
    private Toggle rotateTime;

    public float radius = 75;
    public int hourNum;
    // Use this for initialization
    void Start()
    {
        sundial = GameObject.Find("Sundial").GetComponent<Sundial>();
        earth = GameObject.Find("Earth_8K").GetComponent<Transform>();
        origin = GameObject.Find("Y-Origin").GetComponent<Transform>();
        straight = GameObject.Find("Straight Toggle").GetComponent<Toggle>();
        rotateTime = GameObject.Find("Toggle Rotate Time").GetComponent<Toggle>();
    }
    void Update()
    {
        if ( rotateTime.isOn == false && straight.isOn == false)
        {
            transform.position = new Vector3(
            earth.position.x + 6 / sundial.horizonScale * Mathf.Sin((hourNum * Mathf.PI / 12) + (origin.position.y * Mathf.PI / 180)),
            earth.position.y,
            offset+earth.position.z -6 / sundial.horizonScale * Mathf.Cos((hourNum * Mathf.PI / 12) + (origin.position.y * Mathf.PI / 180) )
            );
        }
        else if (rotateTime.isOn == false && straight.isOn == true)
        {
            transform.position = new Vector3(
            earth.position.x + 6 / sundial.horizonScale * Mathf.Sin(hourNum * Mathf.PI / 12),
            earth.position.y,
            offset + earth.position.z - 6 / sundial.horizonScale * Mathf.Cos(hourNum * Mathf.PI / 12)
            );
        }
        else if (rotateTime.isOn == true && straight.isOn == false)
        {
            transform.position = new Vector3(
            earth.position.x ,
            earth.position.y - 6 / sundial.horizonScale * Mathf.Sin(((hourNum) * Mathf.PI / 12) + (origin.position.x * Mathf.PI / 180)),
            offset + earth.position.z - 6 / sundial.horizonScale * Mathf.Cos(((hourNum) * Mathf.PI / 12) + (origin.position.x * Mathf.PI / 180))
            );
        }
        else if (rotateTime.isOn == true && straight.isOn == true)
        {
            transform.position = new Vector3(
            earth.position.x,
            earth.position.y - 6 / sundial.horizonScale * Mathf.Sin((hourNum + 6) * Mathf.PI / 12),
            offset + earth.position.z - 6 / sundial.horizonScale * Mathf.Cos((hourNum + 6) * Mathf.PI / 12)
            );
        }
        TextObject.LookAt(Camera.main.transform);
    }

}
