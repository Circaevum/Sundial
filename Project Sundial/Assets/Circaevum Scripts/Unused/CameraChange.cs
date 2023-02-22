using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChange : MonoBehaviour {
    public Transform pcCam;
    public Transform vrCam;
	// Use this for initialization
	void Start () {
		
	}
	public void ChangeCameraButtonClicked()
    {
        pcCam.rotation = Quaternion.Euler(-90, 0, 0);
        vrCam.rotation = Quaternion.Euler(-90, 0, 0);
    }
	// Update is called once per frame
	void Update () {
		
	}
}
