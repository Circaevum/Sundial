using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonthLabelPositions : MonoBehaviour {
    private Sundial sundial;
    public GameObject monthLabel;
    // Use this for initialization
    void Start () {
        sundial = GameObject.Find("Sundial").GetComponent<Sundial>();
        monthLabel = GameObject.Find("MonthLabel");
        for (int i = 1; i <= 12; i++)
        {
            InstantiateMonthLabel(i-1);
        }
    }
    public void InstantiateMonthLabel(int monthNum)
    {
        GameObject month = Instantiate(monthLabel);
        month.transform.parent = GameObject.Find("Orbit").transform;
        month.GetComponent<LabelPositioner>().monthNum = monthNum;
        month.GetComponent<TextMesh>().text = sundial.MonthArray[monthNum];
    }
}
