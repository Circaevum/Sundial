using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SunCoordinates : MonoBehaviour {
	private Sundial sundial;
	public Transform cam;
    public float worldHeight;
    public Slider mainSlider;
    private Toggle rotateTime;

    // Use this for initialization
    void Start ()
    {
        sundial = GameObject.Find("Sundial").GetComponent<Sundial>();
        mainSlider = GameObject.Find("Slider").GetComponent<Slider>();
        rotateTime = GameObject.Find("Toggle Rotate Time").GetComponent<Toggle>();
        worldHeight = mainSlider.value;
	}
    // Update is called once per frame
    void Update () {
        if (rotateTime.isOn == true)
        {
            transform.position = new Vector3(cam.position.x /100 * mainSlider.value * 365 / 360, sundial.rotatedShift, 0);
        }
        else if (rotateTime.isOn == false)
        {
            transform.position = new Vector3(0, cam.position.y / 100*mainSlider.value * 365 / 360, 0);
        }
            //Update Sun to follow camera on y-axis
            

	}
}
