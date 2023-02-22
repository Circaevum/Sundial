using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DayLabelMapper : MonoBehaviour
{
    private Sundial sundial;
    public GameObject dayLabel;
    // Use this for initialization
    void Start()
    {
        sundial = GameObject.Find("Sundial").GetComponent<Sundial>();
        for (int i = 0; i < 365; i++)
        {
            InstantiateDayLabel(i);
        }
        dayLabel = GameObject.Find("Day Label");
    }
    public void InstantiateDayLabel(int dayNum)
    {
        GameObject day = Instantiate(dayLabel);
        day.transform.parent = GameObject.Find("Orbit").transform;
        day.GetComponent<DayLabelLocater>().dayNum = dayNum;
        day.GetComponent<TMP_Text>().text = sundial.DayArray[dayNum];
    }
}
