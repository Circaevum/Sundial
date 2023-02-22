using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VRtoggle : MonoBehaviour {
    private Toggle straightToggle;
    private Toggle rotateToggle;
    // Use this for initialization
    public void Start()
    {
        straightToggle = GameObject.Find("Straight Toggle").GetComponent<Toggle>();
        rotateToggle = GameObject.Find("Toggle Rotate Time").GetComponent<Toggle>();
    }
    public void toggleStraight () {
        if (straightToggle.isOn==true)
        {
            GameObject.Find("Straight Toggle").GetComponent<Toggle>().isOn =false;
        }
        else if (straightToggle.isOn == false)
        {
            GameObject.Find("Straight Toggle").GetComponent<Toggle>().isOn=true;
        }
        

    }
    public void toggleRotate()
    {
        if (rotateToggle.isOn == true)
        {
            GameObject.Find("Toggle Rotate Time").GetComponent<Toggle>().isOn = false;
        }
        else if (rotateToggle.isOn == false)
        {
            GameObject.Find("Toggle Rotate Time").GetComponent<Toggle>().isOn = true;
        }


    }

    // Update is called once per frame
    void Update () {
		
	}
}
