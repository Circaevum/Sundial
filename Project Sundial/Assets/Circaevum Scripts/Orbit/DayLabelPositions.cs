using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayLabelPositions : MonoBehaviour
{
    //public GameObject[] monthLabel = new GameObject[12] ;
    public GameObject dayLabel;
    private Sundial sundial;
    private Slider mainSlider;
    // Use this for initialization
    void Start()
    {
        sundial = GameObject.Find("Sundial").GetComponent<Sundial>();
        mainSlider = GameObject.Find("Slider").GetComponent<Slider>();
        //dayLabel = GameObject.Find("DayLabel");
        //mainSlider.onValueChanged.AddListener(delegate { Slider_Changed(mainSlider.value); });
        for (int i = 0; i < 365; i++)
        {
            InstantiateDayLabel(i);
        }
    }
    public void InstantiateDayLabel(int dayNum)
    {
        GameObject day = Instantiate(dayLabel);
        //TextMesh dayText = dayLabel.GetComponent<TextMesh>();
        //dayText.text = sundial.DayArray[dayNum];
        day.GetComponent<DayLabelPositioner>().dayNum = dayNum;
        day.GetComponent<TextMesh>().text = sundial.DayArray[dayNum];
    }
    // Update is called once per frame
    /*
    public void Slider_Changed(float newValue)
    {
        for(int i = 0; i <= 11; i++)
        {
            monthLabel[i].transform.position = new Vector3(
                radius * Mathf.Sin((i * 30 + 15) * Mathf.PI / 180),
                sundial.height * i / 4,
                -radius * Mathf.Cos((i * 30 + 15) * Mathf.PI / 180)
        );
        }
        
    }
    */
}
