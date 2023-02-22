using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EmailThreadManager : MonoBehaviour
{
    public List<string> threadedEmails;
    private LineRenderer threadLine;
    private Slider mainSlider;
    Color newColor;
    // Start is called before the first frame update
    void Start()
    {
        mainSlider = GameObject.Find("Slider").GetComponent<Slider>();
        threadLine = GetComponent<LineRenderer>();
        threadLine.SetWidth(0.01f, 0.01f);
        //int colorInt = Convert.ToInt32("0x"+name.Split(':')[1].Substring(8), 16);
        //float colorFloat = Mathf.Abs((float)colorInt / Mathf.Pow(16,8));
        //Color newColor = new Color(1 - colorFloat, Mathf.Abs(colorFloat) * 2 + 1,colorFloat );
        float rando = new System.Random().Next(0, 100) / 100f;
        Debug.Log("RANDO:" + rando);
        newColor = new Color(1-rando,Mathf.Abs(rando)*2+1,rando);
        Color clearColor = Color.clear;
        var tempMaterial = new Material(threadLine.sharedMaterial);
        tempMaterial.color = clearColor;
        tempMaterial.SetColor("_EmissionColor", clearColor);
        threadLine.sharedMaterial = tempMaterial;
    }

    public void RevealThread()
    {
        var tempMaterial = new Material(threadLine.sharedMaterial);
        tempMaterial.color = Color.green;
        tempMaterial.SetColor("_EmissionColor", Color.green);
        threadLine.sharedMaterial = tempMaterial;
    }
    public void HideThread()
    {
        var tempMaterial = new Material(threadLine.sharedMaterial);
        tempMaterial.color = Color.clear;
        tempMaterial.SetColor("_EmissionColor", Color.clear);
        threadLine.sharedMaterial = tempMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        //Checks if multiple emails have loaded in that are part of this thread
        if (threadedEmails.Count > 1)
        {
            //Establish parameters to build line from
            int count = threadedEmails.Count;
            threadLine.positionCount = count*3-1;
            Transform ClockTransform = transform.parent.GetComponent<EmailEventObject>().transform;

            //Start line at the latest email in thread
            threadLine.GetComponent<LineRenderer>().SetPosition(0, GameObject.Find(threadedEmails[0]).transform.position);
            threadLine.GetComponent<LineRenderer>().SetPosition(1, ClockTransform.position + new Vector3(0,0,GameObject.Find(threadedEmails[0]).transform.position.z / mainSlider.value));
            //Add line segments for each additional email, keeping a trail back to the latest email (maybe switch to being earliest email)
            for (int i = 1; i < count; i++)
            {
                threadLine.GetComponent<LineRenderer>().SetPosition(3 * i - 1, new Vector3(ClockTransform.position.x, ClockTransform.position.y, GameObject.Find(threadedEmails[i]).transform.position.z ));
                threadLine.GetComponent<LineRenderer>().SetPosition(3 * i , GameObject.Find(threadedEmails[i]).transform.position);
                threadLine.GetComponent<LineRenderer>().SetPosition(3 * i + 1, new Vector3(ClockTransform.position.x, ClockTransform.position.y, GameObject.Find(threadedEmails[i]).transform.position.z ));
            }
        }
    }
}
