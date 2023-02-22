using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//"SUNDIAL" is going to be the object we reference to do all of our spatio-temporal conversions
public class Circaevum : MonoBehaviour
{
    public int timeZoneDif = 5;
    public float height = 150f;
    public float wormholeHeight = 60f;
    public float horizonScale = 20f;
    public float rotatedShift = 50;
    
    //Relatively static parameters about what's going on up there
    public float[] distances = new float[] { 36f, 67.24f, 93f, 141.6f, 483.8f, 888.2f, 1787f, 2795f, 3670f };
    public int[] monthLength = new int[] { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31, 31 };
    public float[] Vrot = new float[] { 4.1519f, 1.6255f, 1f, 0.5317f, 0.0843f, 0.0339f, 0.0119f, 0.0061f, 0.0040f };
    public float[] pColor = new float[] { 0x999999, 0xC4BD97, 0x017FFF, 0xED6B00, 0xD8CA9D, 0xD8CA9D, 0x56A9C7, 0x5b5ddf, 0x999999 };      //0x017FFF
    public float[] pDays = new float[] { 87.97f, 224.7f, 365.242f, 686.98f, 4332.59f, 10759.22f, 30685.4f, 60189f, 90465f };
    public float[] worldR = new float[] { 4.878f, 12.104f, 12.756f, 6.787f, 142.796f, 120.660f, 51.118f, 48.6f, 2.274f };
    public string[] MonthArray = new string[] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
    public string[] HebrewMonths = new string[] { "Nisan", "Iyyar", "Sivan", "Tammuz", "Av", "Elul", "Tishri", "Marheshvan", "Kislev", "Tevet", "Shvat", "Adar" };
    public string[] MarsMonthArray = new string[] { "Sagittarius", "Dhanus", "Capricornus", "Makara", "Aquarius", "Kumbha", "Pisces", "Mina", "Aries", "Mesha", "Taurus", "Rishabha", "Gemini", "Mithuna", "Cancer", "Karka", "Leo", "Simha", "Virgo", "Kanya", "Libra", "Tula", "Scorpius", "Vrishika" };
    public string[] HouseArray = new string[] { "Aires", "Taurus", "Gemini", "Cancer", "Leo", "Virgo", "Libra", "Scorpio", "Sagittarius", "Capricorn", "Aquarius", "Pisces" };
    public string[] DayArray = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31", };
    public string[] Weekdays = { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };
    
    
    //Now a bunch of values that we are goiing to go out and find in Start()
    public string timeStamp;        //Get the current UTC time (format:"2020-11-03T18:00:00Z")
    public long unix;               //Find the unix timestamp (number of seconds since Jan 1, 1970 UTC)
    public int dayOfWeek;           //Day of the week as an integer (0=Sunday, 1=Monday, etc.)
    public int localYearUnix;       //The Unix timestamp associated with January 1 of the current year
    public int now;                 //The current time in 5-minute intervals since 2020 or something

    //Start with the most basic: getting the current time. Assign associated values.
    void Start(){
        //
        timeStamp = DateTime.Now.ToString();
        unix = DateTimeOffset.Now.ToUnixTimeSeconds();
        dayOfWeek = ((int)DateTime.Today.DayOfWeek);
        DateTime beginningOfYear = new DateTime((int)DateTime.Now.Year,1,1,0,0,0,DateTimeKind.Utc);
        DateTime nineteen70 = new DateTime(1970,1,1,0,0,0,DateTimeKind.Utc);
        localYearUnix = (int)(beginningOfYear-nineteen70).TotalSeconds;

        //Just establishing the time that all the hour and minute hands point to as the current moment
        string[] time = DateTime.Now.ToString().Split(' ');
        now = UTC2FIVE(DateTime.Now.ToString());
        print(now);
        SundialTest();
    }

    ///<summary>
    /// This thing tests all of the timestamp converters just so we know wtf is going on
    /// </summary>
    public void SundialTest(){
        string input = new DateTime(2021,1,1,0,0,0).ToString();
        Debug.Log(String.Format("|{0,5}|{1,5}|{2,5}|", "FindJulianDay",new DateTime(2021,1,1,0,0,0).ToString(),FindJulianDay(new DateTime(2021,1,1,0,0,0)).ToString()));
        Debug.Log(String.Format("|{0,5}|{1,5}|{2,5}|", "FindJulianDay",new DateTime(2020,1,1,0,0,0).ToString(),FindJulianDay(new DateTime(2020,1,1,0,0,0)).ToString()));
        Debug.Log(String.Format("|{0,5}|{1,5}|{2,5}|", "FindJulianDay",new DateTime(1970,1,1,0,0,0).ToString(),FindJulianDay(new DateTime(1970,1,1,0,0,0)).ToString()));
        Debug.Log(String.Format("|{0,5}|{1,5}|{2,5}|", "FindJulianDay",DateTime.Now.ToString(),FindJulianDay(DateTime.Now).ToString()));
        Debug.Log(String.Format("|{0,5}|{1,5}|{2,5}|", "UTC2Unix",new DateTime(2021,1,1,0,0,0).ToString(),UTC2Unix(new DateTime(2021,1,1,0,0,0).ToString()).ToString()));
        Debug.Log(String.Format("|{0,5}|{1,5}|{2,5}|", "UTC2Unix",new DateTime(2020,1,1,0,0,0).ToString(),UTC2Unix(new DateTime(2020,1,1,0,0,0).ToString()).ToString()));
        Debug.Log(String.Format("|{0,5}|{1,5}|{2,5}|", "UTC2Unix",new DateTime(1970,1,1,0,0,0).ToString(),UTC2Unix(new DateTime(1970,1,1,0,0,0).ToString()).ToString()));
        Debug.Log(String.Format("|{0,5}|{1,5}|{2,5}|", "UTC2Unix",DateTime.Now.ToString(),UTC2Unix(DateTime.Now.ToString()).ToString()));
        Debug.Log(String.Format("|{0,5}|{1,5}|{2,5}|", "UTC2FIVE",new DateTime(2021,1,1,0,0,0).ToString(),UTC2FIVE(new DateTime(2021,1,1,0,0,0).ToString()).ToString()));
        Debug.Log(String.Format("|{0,5}|{1,5}|{2,5}|", "UTC2FIVE",new DateTime(2020,1,1,0,0,0).ToString(),UTC2FIVE(new DateTime(2020,1,1,0,0,0).ToString()).ToString()));
        Debug.Log(String.Format("|{0,5}|{1,5}|{2,5}|", "UTC2FIVE",new DateTime(1970,1,2,0,0,0).ToString(),UTC2FIVE(new DateTime(1970,1,2,0,0,0).ToString()).ToString()));
        Debug.Log(String.Format("|{0,5}|{1,5}|{2,5}|", "UTC2FIVE",DateTime.Now.ToString(),UTC2FIVE(DateTime.Now.ToString()).ToString()));
    }
    
    ///<summary>
    /// Converts "2020-11-03T18:00:00Z" formatted timestamp to a count of seconds since beginning of 2020
    /// <list type="bullet">
    /// <item><description>"2020/1/1T00:00:00" == 0</description></item>
    /// <item><description>"2020/1/1T12:00:00" == 43200</description></item>
    /// <item><description>"2020/1/2T00:00:00" == 86400</description></item>
    /// </list>
    /// </summary>
    public int FindJulianDay(DateTime Date)
    {
        return Date.DayOfYear;
    }

    ///<summary>
    /// Converts "2020-11-03T18:00:00Z" formatted timestamp to a count of seconds since beginning of 2020
    /// <list type="bullet">
    /// <item><description>"2020/1/1T00:00:00" == 0</description></item>
    /// <item><description>"2020/1/1T12:00:00" == 144</description></item>
    /// <item><description>"2020/1/2T00:00:00" == 288</description></item>
    /// </list>
    /// </summary>
    public int UTC2Unix(string incomingDate)
    {
        string[] datetime = incomingDate.Split('T',' ');
        string[] date = datetime[0].Split('-','/');
        int month = Convert.ToInt32(date[1]);
        int day = Convert.ToInt32(date[2]);
        int year = Convert.ToInt32(date[0]);
        if (month < 3)
        {
            month = month + 12;
            year = year - 1;
        }
        //Determine the Julian Day based on current date and time parameters
        int JulianDay = day + (153 * month - 457) / 5 + 365 * year + (year / 4) - (year / 100) + (year / 400) + 1721119;
        //What the heck is this math? 86400 is seconds in a day
        int UnixDate = (JulianDay - 2458104) * 86400 - 16 * 86400 - 365 * 2 * 86400;
        int time = TIME2FIVE(datetime[1]);
        return UnixDate + time;
    }

    ///<summary>
    /// Converts "2020-11-03T18:00:00Z" formatted timestamp to a count of seconds since beginning of 2020
    /// <list type="bullet">
    /// <item><description>"2020/1/1T00:00:00" == 0</description></item>
    /// <item><description>"2020/1/1T12:00:00" == 144</description></item>
    /// <item><description>"2020/1/2T00:00:00" == 288</description></item>
    /// </list>
    /// </summary>
    public int UTC2FIVE(string incomingDate)
    {
        string[] datetime = incomingDate.Split('T',' ');
        int FIVE_date = DATE2FIVE(datetime[0]);
        int FIVE_time;
        if(datetime.Length>1)
            FIVE_time = TIME2FIVE(datetime[1]+datetime[2]);
        else
            FIVE_time = TIME2FIVE(datetime[1]);
        return FIVE_date + FIVE_time;
    }

    /// <summary>
    /// Returns the count of 5-minute increments derived from the date.
    /// <list type="bullet">
    /// <item><description>"1/1/2020" == 0</description></item>
    /// <item><description>"1/2/2020" == 288</description></item>
    /// </list>
    /// </summary>
    public int DATE2FIVE(string ManualDate)
    {
        //Incoming date format: 6/3/2020
        string[] date = ManualDate.Split('/', '-');
        int month = Convert.ToInt32(date[0]);
        int day = Convert.ToInt32(date[1]);
        int year = Convert.ToInt32(date[2]);
        
        //Not sure why this happens, but I think it's relevant for the equation below
        if (month < 3)
        {
            month += 12;
            year--;
        }
        //Here's one reference to this equation: https://quasar.as.utexas.edu/BillInfo/JulianDatesG.html
        //Another equation: https://stackoverflow.com/questions/14218894/number-of-days-between-two-dates-c
        //I add the 288, because there are 288 5-minute increments in 1 day (12 increments per hour * 24 hours)
        int JulianDay = day + (153 * month - 457) / 5 + 365 * year + (year / 4) - (year / 100) + (year / 400) + 1721119;
        int UnixDate = 288*((JulianDay - 2458104) - 16  - 365 * 2);
        Debug.Log("DATE2FIVE:"+ManualDate+" -> "+UnixDate);
        return UnixDate;
    }

    /// <summary>
    /// Breaks apart hour/minute/second data from timestamp and converts to count of 5-minutes increments.
    /// <list type="bullet">
    /// <item><description>"00:00:00 AM" == 0</description></item>
    /// <item><description>"12:00:00 PM" == 144</description></item>
    /// </list>
    /// </summary>
    public int TIME2FIVE(string ManualTime)
    {
        //Incoming timestamp is like this: 8:00:00 AM
        string[] timeString = ManualTime.Split(' ', 'Z');
        string[] time = timeString[0].Split(':');
        int hour = Convert.ToInt32(time[0]);
        int minute = Convert.ToInt32(time[1]);
        //int second = Convert.ToInt32(time[2]);    //We don't care about seconds here for now

        //Add 12 hours if it's "PM"
        if (timeString.Length == 2)
        {
            if (timeString[1] == "PM" && hour != 12)
                hour += 12;
            else if (timeString[1] == "AM" && hour == 12)
                hour = 0;
        }
        
        //Add number of minutes together, and divide by 5 to get the count of 5 minute increments
        int UnixTime = (minute + hour * 60) / 5;
        Debug.Log("TIME2FIVE:"+ManualTime+" -> "+UnixTime);
        return UnixTime;
    }

    /// <summary>
    /// Break up a timestamp that looks like this: "1/18/2011 8:00:00 AM"
    /// </summary>
    public int GTimeToUnixTime(string ManualTime)
    {
        string[] timeString = ManualTime.Split(' ', 'Z');
        string[] time = timeString[1].Split(':');
        int hour = Convert.ToInt32(time[0]);
        int minute = Convert.ToInt32(time[1]);
        //int second = Convert.ToInt32(time[2]);
        if (timeString[2] == "PM" && hour != 12)
            hour += 12;
        else if (timeString[2] == "AM" && hour == 12)
            hour = 0;
        int UnixTime = (minute + hour * 60) / 5;
        return UnixTime;
    }

    /// <summary>
    //Creates new EventObject in scene when called
    /// </summary>
}
