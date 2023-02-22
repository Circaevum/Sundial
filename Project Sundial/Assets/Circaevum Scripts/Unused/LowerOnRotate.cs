using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LowerOnRotate : MonoBehaviour {
    private Toggle rotateTime;
    private Transform orbitalPlaneHeight;
    private Slider mainSlider;
    // Use this for initialization
    void Start () {
        rotateTime = GameObject.Find("Toggle Rotate Time").GetComponent<Toggle>();
        orbitalPlaneHeight = GameObject.Find("Y-Origin").GetComponent<Transform>();
        mainSlider = GameObject.Find("Slider").GetComponent<Slider>();
    }
	
	// Update is called once per frame
	void Update () {
        if (rotateTime.isOn == true)
        {
            transform.position = new Vector3(0, -160, 0);
        }
        else if (rotateTime.isOn == false)
        {
            transform.position = new Vector3(0, orbitalPlaneHeight.position.y*mainSlider.value/33.33333f-50, 0);
        }
    }
}
