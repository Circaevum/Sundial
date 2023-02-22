using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HopToPresent : MonoBehaviour
{
    private Sundial sundial;
    private float start = 0;
    private float focus;

    private float startingPosition;
    private float targetPosition;
    void Start(){
        sundial = GameObject.Find("Sundial").GetComponent<Sundial>();
    }

    public void PresentHop()
    {
        string[] time = DateTime.Now.ToString().Split(' ');
        startingPosition = sundial.now;
        targetPosition = (float)(sundial.DATE2FIVE(time[0]) + sundial.TIME2FIVE(time[1] + " " + time[2]));
        Debug.Log("START:" + startingPosition + "      END:" + targetPosition);
        start = Time.time;
    }
    public void BeginHop()
    {
        string[] time = DateTime.Now.ToString().Split(' ');
        startingPosition = sundial.now;
        targetPosition = (float)(sundial.DATE2FIVE("1/1/2020"));
        Debug.Log("START:" + startingPosition + "      END:" + targetPosition);
        start = Time.time;
    }
    public void EndHop()
    {
        string[] time = DateTime.Now.ToString().Split(' ');
        startingPosition = sundial.now;
        targetPosition = (float)(sundial.DATE2FIVE("1/1/2021"));
        Debug.Log("START:" + startingPosition + "      END:" + targetPosition);
        start = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (start != 0 && Time.time <= start + 2f)
        {
            float elapsedTime = (Time.time - start);
            focus = Mathf.SmoothStep(startingPosition, targetPosition, elapsedTime / 2f);
            Debug.Log("FOCUS:" + focus+ "    START" + startingPosition + "      END:" + targetPosition);
            sundial.now = (int)focus;
        }
        else
        {
            start = 0;
        }
    }
}
