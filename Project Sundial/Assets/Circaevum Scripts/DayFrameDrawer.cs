using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayFrameDrawer : MonoBehaviour
{
    LineRenderer clockRing;
    private Slider mainSlider;
    private float radius = 0.2f;
    public bool flat = true;
    private float heightAdjust = 32.0f;
    private Toggle zoomed;
    private float fromScale;
    private float fromWidth;
    private float toScale;
    private float toWidth;
    private float zoom_start = 0;
    private bool zooming = false;
    void Start()
    {
        mainSlider = GameObject.Find("Slider").GetComponent<Slider>();
        zoomed = GameObject.Find("Zoom Toggle").GetComponent<Toggle>();
        zoomed.onValueChanged.AddListener(delegate { ZoomSystem(); });
        clockRing = GetComponent<LineRenderer>();
        clockRing.positionCount = 100;
        Flatten();
    }

    void Update()
    {
        if (/*OVRInput.GetDown(OVRInput.RawButton.LThumbstick)||*/Input.GetKeyDown("f"))
        {
            flat = !flat;
            if(!flat)
                Unflatten();          
            else
                Flatten();
        }
        if((Input.GetKeyDown("q")||Input.GetKeyDown("e"))&&!flat)
            Unflatten();
        if (zoom_start!=0 && Time.time <= zoom_start + 1f)
        {
            float elapsedTime = (Time.time - zoom_start);
            if(zooming){
                float scale = Mathf.SmoothStep(fromScale, toScale, elapsedTime );
                float lineScale = Mathf.SmoothStep(fromWidth, toWidth, elapsedTime );
                transform.localScale = new Vector3(scale,scale,scale);
                clockRing.startWidth = lineScale;
                clockRing.endWidth = lineScale;
            }
        }
        else
        {
            zooming = false;
            zoom_start = 0;
        }

    }
    void ZoomSystem()
    {
        zooming = true;
            zoom_start = Time.time;
        if (!zoomed.isOn)
        {
            fromScale = 0.1f;
            fromWidth = 0.01f;
            toScale = 1;
            toWidth = 0.001f;
        }
        else
        {
            fromScale = 1;
            fromWidth = 0.001f;
            toScale = 0.1f;
            toWidth = 0.01f;
        }
    }
    void Flatten()
    {
        for (int i = 0; i < 100; i++)
        {
            if (i < 25)
            {
                clockRing.SetPosition(i, new Vector3(
                    radius * Mathf.Sin(i * Mathf.PI / 12),
                    0,
                    -radius * Mathf.Cos(i * Mathf.PI / 12)
                    ));
            }
            else if(i>=25&&i<50)
            {
                clockRing.SetPosition(i, new Vector3(
                (radius + 0.01f) * Mathf.Sin((i-1) * Mathf.PI / 12),
                0,
                -(radius + 0.01f) * Mathf.Cos((i - 1) * Mathf.PI / 12)
                ));
            }
            else if(i>=50&&i<75)
            {
                clockRing.SetPosition(i, new Vector3(
                (radius + 0.01f) * Mathf.Sin((i-2) * Mathf.PI / 12),
                0.003f,
                -(radius + 0.01f) * Mathf.Cos((i - 2) * Mathf.PI / 12)
                ));
            }
            else
            {
                clockRing.SetPosition(i, new Vector3(
                (radius ) * Mathf.Sin((i-3) * Mathf.PI / 12),
                0.003f,
                -(radius ) * Mathf.Cos((i - 3) * Mathf.PI / 12)
                ));
            }
        }
    }
    void Unflatten()
    {
        for (int i = 0; i < 100; i++)
        {
            if (i < 25)
            {
                clockRing.SetPosition(i, new Vector3(
                    radius * Mathf.Sin(i * Mathf.PI / 12),
                    mainSlider.value/heightAdjust*(float)i/1000f,
                    -radius * Mathf.Cos(i * Mathf.PI / 12)
                    ));
            }
            else if(i>=25&&i<50)
            {
                clockRing.SetPosition(i, new Vector3(
                -(radius + 0.01f) * Mathf.Sin((i-1) * Mathf.PI / 12),
                mainSlider.value/heightAdjust*(float)(50-i)/1000f,
                -(radius + 0.01f) * Mathf.Cos((i - 1) * Mathf.PI / 12)
                ));
            }
            else if(i>=50&&i<75)
            {
                clockRing.SetPosition(i, new Vector3(
                (radius + 0.01f) * Mathf.Sin((i-2) * Mathf.PI / 12),
                mainSlider.value/heightAdjust*(float)(i-50)/1000f+0.01f,
                -(radius + 0.01f) * Mathf.Cos((i - 2) * Mathf.PI / 12)
                ));
            }
            else
            {
                clockRing.SetPosition(i, new Vector3(
                -(radius ) * Mathf.Sin((i-3) * Mathf.PI / 12),
                mainSlider.value/heightAdjust*(float)(100-i)/1000f+0.01f,
                -(radius ) * Mathf.Cos((i - 3) * Mathf.PI / 12)
                ));
            }
        }
    }
}
