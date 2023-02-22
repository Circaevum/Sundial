using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MarsCoordinates : MonoBehaviour {
	private Sundial sundial;
	public Transform cam;
    public Slider mainSlider;
    private Toggle rotateTime;
    private Toggle straight;
    
    public int planet;
    // Use this for initialization
    void Start ()
    {
        sundial = GameObject.Find("Sundial").GetComponent<Sundial>();
        mainSlider = GameObject.Find("Slider").GetComponent<Slider>();
        rotateTime = GameObject.Find("Toggle Rotate Time").GetComponent<Toggle>();
        straight = GameObject.Find("Straight Toggle").GetComponent<Toggle>();
	}
    // Update is called once per frame
    void Update () {
        //Update orbital ring to follow camera on y-axis
        if (rotateTime.isOn == true&& straight.isOn == false)
        {
            transform.position = new Vector3( cam.position.x * mainSlider.value / 33.333f, -sundial.distances[3] , 0);
            transform.rotation = Quaternion.Euler( -cam.position.x * 687f,  0 ,- 90f);
        }
        else if (rotateTime.isOn == false)
        {
            transform.position = new Vector3((sundial.distances[3] * Mathf.Sin((212/sundial.Vrot[3]-212+cam.position.y) * 365 / 687 * Mathf.PI / 180f)), cam.position.y * mainSlider.value / 33.333f, -(sundial.distances[3] * Mathf.Cos((212 / sundial.Vrot[3] - 212  + cam.position.y) * 365 / 687 * Mathf.PI / 180f)));
            transform.rotation = Quaternion.Euler(-90f, -cam.position.y * 687f, 0);
        }


    }
}
