using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;
using System;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class AlphaImporter : MonoBehaviour
{
  public List<string> categories = new List<string>();
  private WorldlineFactory worldlineFactory;

  // Use this for initialization
  void Start()
  {
    worldlineFactory = new WorldlineFactory();
  }
  // Update is called once per frame
  void Update()
  {
  }
  public void ParseTxtFile()
  {
    StreamReader reader = File.OpenText("AlphaTimeLog.txt");
    string line;
    int count = 0;
    while ((line = reader.ReadLine()) != null)
    {
      if (count != 0)
      {
        string[] lineValues = line.Split('\t');
        string start = lineValues[0] + " " + lineValues[1];
        string end = lineValues[2] + " " + lineValues[3];
        Color eventColor = Color.clear;
        ColorUtility.TryParseHtmlString(lineValues[6], out eventColor);
        string eventName = lineValues[4];
        string eventDescription = lineValues[8];
        string Category = lineValues[5];
        if (categories.Contains(Category) != true)
          categories.Add(Category);
        string Location = lineValues[7];   
        worldlineFactory.CreateNewEvent(DateTime.Parse(start), DateTime.Parse(end), eventColor, eventName, eventDescription);
        count++;
      }
      else
        count++;
    }
    //Dropdown categoryDropdown = GameObject.Find("Category").GetComponent<Dropdown>();
    //categoryDropdown.options.Clear();
    //for (int i = 0; i < categories.Count; i++)
    //{
    //  categoryDropdown.options.Add(new Dropdown.OptionData(categories[i]));
    //  Debug.Log(categories[i]);
    //}
  }
  int DATE2FIVE(string ManualDate)
  {
    string[] date = ManualDate.Split('/');
    int month = Convert.ToInt32(date[0]);
    int day = Convert.ToInt32(date[1]);
    int year = Convert.ToInt32(date[2]);
    if (month < 3)
    {
      month = month + 12;
      year = year - 1;
    }
    int JulianDay = day + (153 * month - 457) / 5 + 365 * year + (year / 4) - (year / 100) + (year / 400) + 1721119;
    Debug.Log(JulianDay);
    int UnixDate = (JulianDay - 2458104) * 288 - 17 * 288 - 365 * 288;
    Debug.Log(UnixDate);
    return UnixDate;
  }
  int TIME2FIVE(string ManualTime)
  {
    string[] timeString = ManualTime.Split(' ');
    string[] time = timeString[0].Split(':');
    int hour = Convert.ToInt32(time[0]);
    int minute = Convert.ToInt32(time[1]);
    //int second = Convert.ToInt32(time[2]);
    if (timeString[1] == "PM" && hour != 12)
      hour += 12;
    else if (timeString[1] == "AM" && hour == 12)
      hour = 0;
    int UnixTime = (minute + hour * 60) / 5;
    return UnixTime;
  }
}
