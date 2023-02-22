using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrbitTextObject : MonoBehaviour
{
    public Transform OrbitalTextObject;
    private Slider mainSlider;
    private Sundial sundial;
    private GameObject clock;
    private GameObject TimeClock;

    public float radius;
    public int start;
    public int stop;
    public float time;
    public int planet;
    private float inc = 12;
    // Use this for initialization
    void Start()
    {
        sundial = GameObject.Find("Sundial").GetComponent<Sundial>();
        clock = GameObject.Find("Clock");
        mainSlider = GameObject.Find("Slider").GetComponent<Slider>();
        radius = clock.GetComponent<ClockValues>().radius/2;
    }

    // Update is called once per frame
    void Update()
    {
        time = sundial.now;
        float k = ((start) + (stop - start) / 2f);
        //float distance = Mathf.Abs(- (k * radius / 4800f / inc * (360f / 365f) ));
        if (Mathf.Abs((start - time * 288) / 240f / inc * mainSlider.value / 4) <= 5)
        {
            GetComponent<TextMesh>().characterSize = 0.3f;
            transform.position = clock.transform.position+new Vector3(
                    radius * sundial.distances[planet] / sundial.horizonScale * Mathf.Sin(k / 288f * Mathf.PI / 180),
                    0.05f,
                    radius * sundial.distances[planet] / sundial.horizonScale * Mathf.Cos(k / 288f * Mathf.PI / 180)
                );
            transform.LookAt(Camera.main.transform);
        }
        else
            GetComponent<TextMesh>().characterSize = 0;
    }

}
