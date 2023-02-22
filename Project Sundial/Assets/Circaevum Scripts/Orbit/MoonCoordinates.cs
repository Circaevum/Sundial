using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MoonCoordinates : MonoBehaviour
{
    private Sundial sundial;
    public Transform cam;
    public Slider mainSlider;
    private Toggle rotateTime;
    private float moonX = 10f;
    // Use this for initialization
    void Start()
    {
        sundial = GameObject.Find("Sundial").GetComponent<Sundial>();
        mainSlider = GameObject.Find("Slider").GetComponent<Slider>();
        rotateTime = GameObject.Find("Toggle Rotate Time").GetComponent<Toggle>();
    }
    // Update is called once per frame
    void Update()
    {
        if (rotateTime.isOn == true)
        {
            transform.position = new Vector3(
                cam.position.x * mainSlider.value / 33.333f,
                -sundial.distances[2] + moonX * Mathf.Sin((cam.position.x + 130f / 24f) * 11f * Mathf.PI / 180f),
                - moonX * Mathf.Cos((cam.position.x + 130f / 24f) * 11f * Mathf.PI / 180f));
            transform.rotation = Quaternion.Euler(-cam.position.y * 13.36f, 0, -90f);
        }
        else if (rotateTime.isOn == false)
        {
            transform.position = new Vector3(
                (sundial.distances[2] * Mathf.Sin((cam.position.y) * Mathf.PI / 180f)) + moonX * Mathf.Sin((cam.position.y + 130f / 24f) * 11f * Mathf.PI / 180f),
                cam.position.y * mainSlider.value / 33.333f,
                -(sundial.distances[2] * Mathf.Cos((cam.position.y) * Mathf.PI / 180f)) - moonX * Mathf.Cos((cam.position.y + 130f / 24f) * 11f * Mathf.PI / 180f));
            transform.rotation = Quaternion.Euler(-90f, -cam.position.y * 13.36f, 0);
        }


        //Update orbital ring to follow camera on y-axis
        //transform.position = new Vector3(0, cam.position.y* sundialHeight / 33.333f, -sundial.distances[2] );
        //transform.rotation = Quaternion.Euler(-90f,-cam.position.y*365f, 0);

        //Closest to tile were (-90,0,0) followed by (-67,90,-90)
    }

}
