using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FollowHead : MonoBehaviour
{
	public Transform cam;
    private Sundial sundial;
    public float worldHeight;
    public Slider mainSlider;


    void Start()
    {
        mainSlider = GameObject.Find("Slider").GetComponent<Slider>();
        mainSlider.onValueChanged.AddListener(delegate { Slider_Changed(mainSlider.value); });
        worldHeight = mainSlider.value;
        sundial = GameObject.Find("Sundial").GetComponent<Sundial>();
    	//Update orbital ring to follow camera on y-axis
        transform.position = new Vector3(transform.position.x, cam.position.y* mainSlider.value / 33.333f, transform.position.z);
    }
    public void Slider_Changed(float newValue)
    {
        worldHeight = newValue;
    }

    void Update()
    {
        //Update orbital ring to follow camera on y-axis
        transform.position = new Vector3(transform.position.x, cam.position.y* worldHeight / 33.333f, transform.position.z);
    }

}
