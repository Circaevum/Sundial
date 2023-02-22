using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CustomControls : MonoBehaviour
{
    private Sundial sundial;
    public GameObject player;
    private GameObject clock;
    private Toggle straight;
    private Toggle rotateTime;
    private Toggle zoom;
    public float ThrustForce;
    //public Rigidbody NaviBase;
    private Slider mainSlider;
    private void Start()
    {
        sundial = GameObject.Find("Sundial").GetComponent<Sundial>();
        mainSlider = GameObject.Find("Slider").GetComponent<Slider>();
        straight = GameObject.Find("Straight Toggle").GetComponent<Toggle>();
        rotateTime = GameObject.Find("Toggle Rotate Time").GetComponent<Toggle>();
        zoom = GameObject.Find("Zoom Toggle").GetComponent<Toggle>();
    }
    // Update is called once per frame
    void Update()
    {
        ///float forceMultiplier = ThrustForce * mainSlider.value * (360f / 365f);
        if (Input.GetKeyDown("space"))
        {
            Debug.Log("HOPPER_CALLED");
            GameObject.Find("NOW").GetComponent<HopToPresent>().PresentHop();
        }

                
        if (Input.GetKeyDown(KeyCode.DownArrow))
            if(player.transform.localScale.x <=50)
                player.transform.localScale += player.transform.localScale / 7;
        if (Input.GetKeyDown(KeyCode.UpArrow))
            if (player.transform.localScale.x >=0.5f)
                player.transform.localScale -= player.transform.localScale/8;
        if (Input.GetKeyDown("e"))
            mainSlider.value = mainSlider.value * 2;
        if (Input.GetKeyDown("q"))
            mainSlider.value = mainSlider.value / 2;
        if (Input.GetKey("w"))
        {
            sundial.now+=512/(int)mainSlider.value;
        }
        if (Input.GetKey("s"))
        {
            sundial.now-= 512 / (int)mainSlider.value;
        }
        if (Input.GetKeyDown("r"))
        {
            GameObject.Find("Toggle Rotate Time").GetComponent<AudioSource>().Play();
            if (rotateTime.isOn == true)
                rotateTime.isOn = false;
            else
                rotateTime.isOn = true;
        }
        if (Input.GetKeyDown("t"))
        {
            if (straight.isOn == true)
                straight.isOn = false;
            else
                straight.isOn = true;
        }
        if (Input.GetKeyDown("z"))
        {
            if (zoom.isOn == true)
                zoom.isOn = false;
            else
                zoom.isOn = true;
        }
    }
}
