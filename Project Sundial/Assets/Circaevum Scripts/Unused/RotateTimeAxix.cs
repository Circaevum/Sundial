using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotateTimeAxix : MonoBehaviour {
    private Toggle straight;
    private Toggle rotateTime;
    private Sundial sundial;
    private Transform timeAxis;
    // Use this for initializationzy
    void Start () {
        straight = GameObject.Find("Straight Toggle").GetComponent<Toggle>();
        //straight.onValueChanged.AddListener(delegate { RotateTime(); });
        timeAxis = GameObject.Find("Things to Rotate").GetComponent<Transform>();
        rotateTime = GameObject.Find("Toggle Rotate Time").GetComponent<Toggle>();
        rotateTime.onValueChanged.AddListener(delegate { RotateTime(); });
        sundial = GameObject.Find("Sundial").GetComponent<Sundial>();
    }
	void RotateTime()
    {
        if(rotateTime.isOn == true)
        {
            timeAxis.Rotate(new Vector3(0, 0, -90));
            timeAxis.position = new Vector3(0, sundial.rotatedShift, 0);
        }
        else if(rotateTime.isOn == false)
        {
            timeAxis.Rotate(new Vector3(0, 0, 90));
            timeAxis.position = new Vector3(0, 0, 0);
        }
    }
    // Update is called once per frame
    void Update () {
		
	}
}
