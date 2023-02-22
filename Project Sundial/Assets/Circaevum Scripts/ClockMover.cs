using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClockMover : MonoBehaviour
{    
	private Sundial sundial;
    private GameObject clock;
    public float time;
    private Vector3 position= new Vector3(0,0,0);

    private void Start()
    {
        sundial = GameObject.Find("Sundial").GetComponent<Sundial>();
    }
    private void Update()
    {
        time = sundial.now*360/sundial.pDays[2];
        transform.position = new Vector3(0,0,0);
        transform.eulerAngles = new Vector3(0,0,-time* Mathf.PI / 180f*288f/5f / 360f);
               
    }
}
