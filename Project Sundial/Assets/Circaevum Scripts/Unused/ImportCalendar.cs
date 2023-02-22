using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;
using System;

public class ImportCalendar : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        ParseTxtFile();
    }
    // Update is called once per frame
    void Update()
    {
    }
    void ParseTxtFile()
    {
        StreamReader reader = File.OpenText("Calendar.txt");
        string line;
        int count = 0;
        while ((line = reader.ReadLine()) != null)
        {
            if (count != 0)
            {
                Debug.Log(line);
                string[] lineValues = line.Split('\t');
                Debug.Log("lineValues[0]: " + lineValues[0]);
                int SD = Convert.ToInt32(ConvertDT(lineValues[0]));
                int ED = Convert.ToInt32(ConvertDT(lineValues[1]));
                Debug.Log("SD: " + SD + "\nED" + ED);
                Color eventColor = Color.clear;
                ColorUtility.TryParseHtmlString(lineValues[2], out eventColor);
                string eventSummary = lineValues[3];
                InstantiateButton newEvent = new InstantiateButton();
                newEvent.CreateNewEvent(SD, ED, eventColor, eventSummary);
                count++;
            }
            else
                count++;
            
        }
    }
    static int ConvertDT(string UTC)
    {
        int DTyy = Convert.ToInt32(UTC.Substring(0, 4));
        int DTmm = Convert.ToInt32(UTC.Substring(4, 2));
        int DTdd = Convert.ToInt32(UTC.Substring(6, 2));
        int DThh = Convert.ToInt32(UTC.Substring(9, 2));
        int DTmi = Convert.ToInt32(UTC.Substring(11, 2));
        int DTss = Convert.ToInt32(UTC.Substring(13, 2));
        Debug.Log("Start(yyyy): " + DTyy + "\n(mm):" + DTmm + "\n(dd):" + DTdd + "\n(hh):" + DThh + "\n(mm):" + DTmi + "\n(ss):" + DTss);
        //DateTime newUTC = new DateTime(DTyy, DTmm, DTdd, DThh, DTmm, DTss, DateTimeKind.Utc);
        //Debug.Log("DateTime: " + newUTC);
        if (DTmm < 3)
        {
            DTmm = DTmm + 12;
            DTyy = DTyy - 1;
        }
        int JulianDay = DTdd + (153 * DTmm - 457) / 5 + 365 * DTyy + (DTyy / 4) - (DTyy / 100) + (DTyy / 400) + 1721119;
        Debug.Log("Julian Day: " + JulianDay);
        int dateTime = (DTmi + DThh * 60) / 5 + 288 * (JulianDay-2458104);
        Debug.Log("Return dateTime: " + dateTime);
        return dateTime-4608;
    }

}
