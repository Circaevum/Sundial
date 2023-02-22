using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextLongFollowObject : MonoBehaviour
{
    public Transform LongTextObject;
    private Slider mainSlider;
    private Circaevum sundial;
    private GameObject clock;

    public float radius;
    public int start;
    public int stop;
    public float time;
    private float inc = 12;
    // Use this for initialization
    void Start()
    {
        sundial = GameObject.Find("Sundial").GetComponent<Circaevum>();
        clock = GameObject.Find("Clock");
        mainSlider = GameObject.Find("Slider").GetComponent<Slider>();
        radius = clock.GetComponent<ClockValues>().radius;
    }

    // Update is called once per frame
    void Update()
    {
        time = sundial.now;
        float k = ((start) + (stop - start) / 2f);
        //float distance = Mathf.Abs(- (k * radius / 4800f / inc * (360f / 365f) ));
        if (Mathf.Abs((start - time) / 240f / inc * mainSlider.value / 4) <= 5)
        {
            GetComponent<TextMesh>().characterSize = 1;
            transform.position = clock.transform.position + new Vector3(
                    0,
                    radius * 1.3f,
                    (k - time) / 240f / inc * mainSlider.value / 4
                );
            transform.LookAt(Camera.main.transform);
        }
        else
            GetComponent<TextMesh>().characterSize = 0;
    }

}
