using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayLabelPositioner : MonoBehaviour
{
    private Sundial sundial;
    public Transform TextObject;
    private Slider mainSlider;
    public GameObject TimeClock;
    private Transform earth;
    public int dayNum;
    public float time;
    private float inc = 12;
    // Use this for initialization
    void Start()
    {
        sundial = GameObject.Find("Sundial").GetComponent<Sundial>();
        mainSlider = GameObject.Find("Slider").GetComponent<Slider>();
        earth = GameObject.Find("Earth_8K").GetComponent<Transform>();
        transform.position = transform.parent.transform.position + new Vector3(
                transform.parent.transform.localScale.x* sundial.distances[2] / sundial.horizonScale * Mathf.Sin(dayNum * Mathf.PI / 180 * 360 / 365),
                -transform.parent.transform.localScale.x * sundial.distances[2] / sundial.horizonScale * Mathf.Cos(dayNum * Mathf.PI / 180 * 360 / 365),
                0
           );
    }
    
    void Update()
    {
        time = sundial.now;
        
        /*
        if(straight.isOn == false && rotateTime.isOn == false)
        {
            transform.position = new Vector3(
               sundial.distances[2] / sundial.horizonScale * Mathf.Sin(dayNum * Mathf.PI / 180 * 360 / 365),
               mainSlider.value * dayNum / 100f ,
               -sundial.distances[2] / sundial.horizonScale * Mathf.Cos(dayNum * Mathf.PI / 180 * 360 / 365)
          );
        }
        else if (straight.isOn == false && rotateTime.isOn == true)
        {
            transform.position = new Vector3(
               mainSlider.value  * dayNum / 100f ,
               -sundial.distances[2] / sundial.horizonScale * Mathf.Sin(dayNum * Mathf.PI / 180 * 360 / 365)+sundial.rotatedShift,
               -sundial.distances[2] / sundial.horizonScale * Mathf.Cos(dayNum * Mathf.PI / 180 * 360 / 365)
          );
        }
        else if (straight.isOn == true && rotateTime.isOn == false)
        {
            transform.position = new Vector3(
                earth.position.x * 0.9f,
                mainSlider.value * dayNum / 100f ,
                earth.position.z * 0.9f
           );
        }
        else if (straight.isOn == true && rotateTime.isOn == true)
        {
            transform.position = new Vector3(
                mainSlider.value * dayNum / 100f ,
                earth.position.y * 0.9f,
                earth.position.z * 0.9f
           );
        }
        */
        transform.LookAt(Camera.main.transform);
    }

}
