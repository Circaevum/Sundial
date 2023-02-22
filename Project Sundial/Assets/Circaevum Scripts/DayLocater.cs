using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayLocater : MonoBehaviour
{
    private Sundial sundial;
    private GameObject ParentObject;
    private GameObject clock;
    private Slider mainSlider;
    private Toggle rotated;
    private Toggle straight;
    private Vector3 position= new Vector3(0,0,0);
    public float start=0;
    public int julianDay;
    public int unixStart;
    public int unixStop;
    public int dayOfWeek;
    public int unixSunrise;
    public int unixSunset;
    private Vector3 fromPosition;
    private Vector3 toPosition;
    private Boolean rotating = false;
    private Boolean straightening = false;
    public Tuple<float,float> coordinates;
    private float time;
    private float radius;
    // Use this for initialization
    void Start()
    {
        sundial = GameObject.Find("Sundial").GetComponent<Sundial>();
        ParentObject = GameObject.Find("Orbit");
        //string[] time = DateTime.Now.ToString().Split(' ');
        mainSlider = GameObject.Find("Slider").GetComponent<Slider>();
        radius = ParentObject.GetComponent<ClockValues>().radius/1.84f;
        sundial = GameObject.Find("Sundial").GetComponent<Sundial>();
        transform.localScale = transform.localScale.y * new Vector3(
            -transform.parent.transform.localScale.x,
            transform.parent.transform.localScale.y,
            transform.parent.transform.localScale.z
            );
        transform.eulerAngles = new Vector3(0,0/*-(julianDay) * 360 / 365*/,0);
        rotated = GameObject.Find("Toggle Rotate Time").GetComponent<Toggle>();
        straight = GameObject.Find("Straight Toggle").GetComponent<Toggle>();
        rotated.onValueChanged.AddListener(delegate { MoveSystem(); });
        straight.onValueChanged.AddListener(delegate { MoveSystem(); });
    }
    void AdhereToDayParameters(){
        
    }
    void MoveSystem(){
        straightening = true;
        start = Time.time;
        Vector3 globalScale = ParentObject.transform.parent.localScale;
        Vector3 earth = GameObject.Find("Earth_8K").transform.position;
        fromPosition = transform.position;
        
        if(!rotated.isOn&&!straight.isOn){
            toPosition = new Vector3(
               globalScale.x*ParentObject.transform.localScale.x * radius *sundial.distances[2] / sundial.horizonScale * 0.92f * Mathf.Sin(julianDay * Mathf.PI / 180 * 360 / 365),
               (julianDay-time/288f%365f) * mainSlider.value/64f/4f,
               globalScale.x*ParentObject.transform.localScale.x * -radius*sundial.distances[2] / sundial.horizonScale * 0.92f * Mathf.Cos(julianDay * Mathf.PI / 180 * 360 / 365)
          );
        }else if(!rotated.isOn&&straight.isOn){
            toPosition = new Vector3(
               earth.x,
               (julianDay-time/288f%365f) * mainSlider.value/64f/4f,
               earth.z
          );
        }else if(rotated.isOn&&!straight.isOn){
            toPosition = new Vector3(
               globalScale.x*ParentObject.transform.localScale.x * radius *sundial.distances[2] / sundial.horizonScale * 0.92f * Mathf.Sin(julianDay * Mathf.PI / 180 * 360 / 365),
               globalScale.x*ParentObject.transform.localScale.x * radius*sundial.distances[2] / sundial.horizonScale * 0.92f * Mathf.Cos(julianDay * Mathf.PI / 180 * 360 / 365),
               (julianDay-time/288f%365f) * mainSlider.value/64f/4f
          );
        }else{
            toPosition = new Vector3(
               earth.x,
               -earth.z,
               (julianDay-time/288f%365f) * mainSlider.value/64f/4f
          );
        }
    }
    void Update(){
        time = sundial.now*360/sundial.pDays[2];
        Vector3 earth = GameObject.Find("Earth_8K").transform.position;
        
        //If the rotating or zooming functions were activated within 4 seconds, then do the positional transfers
        if (start!=0 && Time.time <= start + 1f)
        {
            float elapsedTime = (Time.time - start);
            if(rotating||straightening){
                position = new Vector3(
                    Mathf.SmoothStep(fromPosition.x, toPosition.x, elapsedTime ),
                    Mathf.SmoothStep(fromPosition.y, toPosition.y, elapsedTime ),
                    Mathf.SmoothStep(fromPosition.z, toPosition.z, elapsedTime )
                );
                transform.position = position;
            }
        }else{
            rotating = false;
            straightening = false;
            time = sundial.now;
            Vector3 globalScale = ParentObject.transform.parent.localScale;
            if(!rotated.isOn&&straight.isOn){
                transform.position = new Vector3(
                    globalScale.x*earth.x,
                    globalScale.x*(julianDay-time/288f%365f) * mainSlider.value/64f/4f,
                    globalScale.x*earth.z
                );
            }else if(!rotated.isOn&&!straight.isOn){
                transform.position = GameObject.Find("Orbit").transform.position + new Vector3(
                    globalScale.x*ParentObject.transform.localScale.x * radius *sundial.distances[2] / sundial.horizonScale * 0.92f * Mathf.Sin(julianDay * Mathf.PI / 180 * 360 / 365),
                    globalScale.x*(julianDay-time/288f%365f) * mainSlider.value/64f/4f,
                    globalScale.x*ParentObject.transform.localScale.x * -radius*sundial.distances[2] / sundial.horizonScale * 0.92f * Mathf.Cos(julianDay * Mathf.PI / 180 * 360 / 365)
                );
            }else if(rotated.isOn&&straight.isOn){
                transform.position = new Vector3(
                    globalScale.x*earth.x,
                    globalScale.x*earth.y,
                    globalScale.x*(julianDay-time/288f%365f) * mainSlider.value/64f/4f
                );
            }else if(rotated.isOn&&!straight.isOn){
                transform.position = GameObject.Find("Orbit").transform.position + new Vector3(
                    globalScale.x*ParentObject.transform.localScale.x * radius *sundial.distances[2] / sundial.horizonScale * 0.92f * Mathf.Sin(julianDay * Mathf.PI / 180 * 360 / 365),
                    globalScale.x*ParentObject.transform.localScale.x * radius*sundial.distances[2] / sundial.horizonScale * 0.92f * Mathf.Cos(julianDay * Mathf.PI / 180 * 360 / 365),
                    globalScale.x*(julianDay-time/288f%365f) * mainSlider.value/64f/4f
                );
            }
            transform.localScale = globalScale;
        }
    }
}
