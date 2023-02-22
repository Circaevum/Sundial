using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstantiateLongEvents : MonoBehaviour
{
    public GameObject LongTermEvent;
    public GameObject LongTextObject;

    void Start()
    {

    }
    public void InstantiateLongEventButtonClicked()
    {
        //CreateLongEvent(15552, 15630, Color.blue, "Sleep", 8);
        CreateLongEvent(50908, 70560, Color.gray, "Africa Trip", -10);
        CreateLongEvent(50908, 51233, Color.cyan, "Flights to Ghana", -12);
        CreateLongEvent(51372, 52344, Color.yellow, "IEEE Power Africa", -16);
        CreateLongEvent(51233, 61608, Color.green, "Ghana", -14);
        CreateLongEvent(61884, 63282, Color.green, "The Netherlands", -14);
        CreateLongEvent(63306, 65232, Color.green, "Germany", -14);
        CreateLongEvent(68940, 69912, Color.yellow, "TED Global 2017", -16);
        CreateLongEvent(65520, 70704, Color.green, "Tanzania", -14);
        CreateLongEvent(61608, 61893, Color.cyan, "Flights to Kenya", -12);
        CreateLongEvent(41865, 41882, Color.cyan, "Flight to Colorado", -12);
        CreateLongEvent(41882, 43419, Color.green, "Colorado", -14);
        CreateLongEvent(43419, 43439, Color.cyan, "Flight to San Francisco", -12);
        CreateLongEvent(43439, 44480, Color.green, "Silicon Valley", -14);
        CreateLongEvent(44480, 44591, Color.cyan, "Flight to Columbus", -12);
        CreateLongEvent(43884, 44340, Color.yellow, "Augmented World Expo", -16);
        CreateLongEvent(57024, 58416, Color.magenta, "Africa Open Data Conference", -16);
        CreateLongEvent(94257, 94383, Color.yellow, "Flights to Amsterdam", -12);
        CreateLongEvent(94320, 95016, Color.green, "VR Days: Europe", -14);
        CreateLongEvent(95184, 95760, Color.cyan, "Antwerp Trip", -14);
        CreateLongEvent(97987, 98124, Color.yellow, "Flights to Pittsburgh", -12);
        CreateLongEvent(94257, 98124, Color.gray, "Netherlands Trip", -10);

    }
    public void RPO()
    {
        CreateLongEvent(95268, 130692, Color.red, "Ready Player One Event Preparation", 5);
        CreateLongEvent(95268, 130692, Color.red, "Ready Player One Event Preparation", -5);
    }
    public void CreateLongEvent(int tStart, int tStop, Color color1, string eventName, float tRadius)
    {
        LongTermEvent = GameObject.Find("LongTermEvent");
        GameObject event1 = Instantiate(LongTermEvent);
        LongTextObject = GameObject.Find("LongTextObject");
        GameObject text1 = Instantiate(LongTextObject);
        //EventCanvas = GameObject.Find("EventCanvas");
        //EventText = GameObject.Find("EventText");

        //GameObject canvas1 = Instantiate(EventCanvas);
        //GameObject text1 = Instantiate(EventText);
        //Text eventText = Instantiate(EventText, pos, rot) as Text;
        //tempTextBox.transform.SetParent(Canvas.transform, false);
        //GameObject tempTextBox = (GameObject)Instantiate(distanceText);
        //TextMesh theText = tempTextBox.transform.GetComponent<TextMesh>();
        //theText.text = eventName;

        Renderer rend = event1.GetComponent<Renderer>();
        rend.material.color = color1;
        rend.material.SetColor("_EmissionColor", color1);
        //text1.

        //Material mat1 = new Material(shader);
        //mat1.color = color1;
        //event1.SetActive(true);
        event1.GetComponent<LongTermEvent>().start = tStart;
        event1.GetComponent<LongTermEvent>().stop = tStop;
        event1.GetComponent<LongTermEvent>().radius = tRadius;
        text1.GetComponent<TextLongFollowObject>().start = tStart;
        text1.GetComponent<TextLongFollowObject>().stop = tStop;
        text1.GetComponent<TextLongFollowObject>().radius = tRadius;

        TextMesh text2 = text1.GetComponent<TextMesh>();
        text2.text = eventName;
        text2.color = color1;
        //event1.GetComponent<StraightEventModeler>().GetComponent<Material>().color = Color.red;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
