using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EarthCoordinates : MonoBehaviour {
	private Sundial sundial;
	public Transform cam;
    public Slider mainSlider;
    private Toggle rotateTime;
    private Toggle straight;
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
        if(rotateTime.isOn == true && straight.isOn == true)
        {
            transform.position = new Vector3(
                cam.position.x /100 * mainSlider.value * 365 / 360, 
                -sundial.distances[2] / sundial.horizonScale+ sundial.rotatedShift, 
                0
            );
            transform.rotation = Quaternion.Euler( 0, 90f, -cam.position.x * 360 - 90f);
        }
        else if (rotateTime.isOn == true && straight.isOn == false)
        {
            transform.position = new Vector3(
                cam.position.x / 100* mainSlider.value * 365 / 360, 
                -sundial.distances[2] / sundial.horizonScale * Mathf.Sin(cam.position.x * Mathf.PI / 180f * 360f / 365f) +sundial.rotatedShift,
                -sundial.distances[2] / sundial.horizonScale * Mathf.Cos(cam.position.x * Mathf.PI / 180f * 360f / 365f));
            transform.rotation = Quaternion.Euler(0, 90f, (-cam.position.x * 360f - cam.position.x *360f/ 365f) );
        }
        else if (rotateTime.isOn == false && straight.isOn == true)
        {
            transform.position = new Vector3(
                0,
                cam.position.y/100 * mainSlider.value*365/360,
                -sundial.distances[2] / sundial.horizonScale
            );
            transform.rotation = Quaternion.Euler(-90f, -cam.position.y * 360, 90f);
        }
        else if(rotateTime.isOn == false && straight.isOn == false)
        {
            transform.position = new Vector3(
                sundial.distances[2] / sundial.horizonScale * Mathf.Sin(cam.position.y * Mathf.PI / 180f * 360f / 365f), 
                cam.position.y/100 * mainSlider.value * 365 / 360, 
                -sundial.distances[2] / sundial.horizonScale * Mathf.Cos(cam.position.y * Mathf.PI / 180f* 360f / 365f));
            transform.rotation = Quaternion.Euler(-90f, (-cam.position.y * 360 - cam.position.y *360/ 365), 90f);
        }

            
        //Update orbital ring to follow camera on y-axis
        //transform.position = new Vector3(0, cam.position.y* sundialHeight / 33.333f, -sundial.distances[2] );
        //transform.rotation = Quaternion.Euler(-90f,-cam.position.y*365f, 0);

        //Closest to tile were (-90,0,0) followed by (-67,90,-90)
    }

}
