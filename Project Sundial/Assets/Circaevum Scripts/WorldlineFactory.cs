using System.Globalization;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldlineFactory : MonoBehaviour
{
    private Sundial sundial;
    public void Awake(){
        sundial = GameObject.Find("Sundial").GetComponent<Sundial>();
    }
    public IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
        {
            for(var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                yield return day;
        }
    public void CreateNewEvent(DateTime start, DateTime stop, Color color, string name, string body)
    {
        sundial = GameObject.Find("Sundial").GetComponent<Sundial>();
        GameObject event1;
        GameObject text1;
        double span = (stop-start).TotalDays;
        double radius = 2/(span+1)+0.3f;
        string justDate = start.ToString();
        Debug.Log("TIMESTAMP:"+span+" RADIUS:"+radius);
        int FIVE_start = sundial.UTC2FIVE(start.ToString())%288;
        int FIVE_stop = sundial.UTC2FIVE(stop.ToString())%288;
        int FIVE_dayEnd = FIVE_start-FIVE_start%288+288;
        int julianDay = (FIVE_start-FIVE_start%288)-sundial.localYearUnix;
        string startDisc = "CIRCA_"+julianDay;
        GameObject superEvent = GameObject.Find(startDisc);

        //Out of the gates, if the timestamps for start and stop are the same,
        //Let's make the event default to 5 minutes (1 increment) so it shows up in the first place
        if ((stop - start).TotalMinutes < 5)
        {
            stop = start.AddMinutes(5);
        }

        //For every day that this event occupies, create an event worldline on that event
        foreach (DateTime day in EachDay(start, stop)){
            //Establish the FIVE_start of each day
            //IF it's the first day of the event, FIVE_start should equal "start"
            //ELSE, FIVE_start should equal "day"
            FIVE_start = sundial.UTC2FIVE(start.ToString())%288;

            //Establish the FIVE_stop of each day
            //IF it's not the last day of the event, FIVE_stop should equal "day+1"
            //ELSE, FIVE_stop should equal "stop"
            FIVE_stop = sundial.UTC2FIVE(stop.ToString())%288;

            //Check that sleep started in AM
            if(FIVE_start>FIVE_stop)
                FIVE_start = 0;


            //Figure out the Julian Day for this DateTime day
            julianDay = sundial.FindJulianDay(day);
            Debug.Log("INCOMING DATETIMES:"+start.ToString()+":"+stop.ToString()+"\n5TIME:"+FIVE_start+":"+FIVE_stop+" \nJULIAN_DAY:"+julianDay+"\nNAME:"+name);
            GameObject parent = GameObject.Find("CIRCA_"+julianDay);
            event1 = Instantiate(GameObject.Find("EventObject"));
            event1.transform.parent = parent.transform;
            event1.transform.rotation = parent.transform.rotation;
            event1.transform.position = parent.transform.position;
            event1.GetComponent<LineRenderer>().positionCount = Mathf.Abs(FIVE_stop - FIVE_start);
            event1.GetComponent<ShortTermEvent>().start = FIVE_start;
            event1.GetComponent<ShortTermEvent>().stop = FIVE_stop;
            event1.name = name;
            event1.GetComponent<ShortTermEvent>().body = body;
            event1.GetComponent<ShortTermEvent>().radius = (float)radius;
            text1 = event1.transform.GetChild(0).gameObject;
            text1.GetComponent<TextFollowObject>().start = FIVE_start;
            text1.GetComponent<TextFollowObject>().stop = FIVE_stop;
            text1.GetComponent<TextFollowObject>().radius = (float)radius;
            //event1.transform.parent = GameObject.Find("Clock").transform;
            //event1.transform.position = GameObject.Find("Clock").transform.position;
            
            Renderer rend = event1.GetComponent<Renderer>();
            var tempMaterial = new Material(rend.sharedMaterial);
            tempMaterial.color = color;
            float emission = Mathf.PingPong(Time.time, 1.0f);
            Color finalColor = color * Mathf.LinearToGammaSpace(emission);
            tempMaterial.SetColor("_EmissionColor", color);
            rend.sharedMaterial = tempMaterial;

            text1.GetComponent<TextMesh>().text = name;
            text1.GetComponent<TextMesh>().color = color;
        }
        //Next, we want to figure out if it's an event that is fully encompassed in one day, or rolls over into multiple days


        //Next condition, if it's an event that spans multiple days, then let's make a Worldline for each day
        //The radius will be inversely proportional to the length of the event, so shorter events have a big radius,
        //and longer ones get smaller towards the center of the circle.
        
        //If the event is longer than 1 day, create a simpler EventObject for it on the long-term annual event helix
        if((stop - start).TotalDays>1)
        {
            event1 = Instantiate(GameObject.Find("LongTermEvent"));
            event1.transform.parent = GameObject.Find("Clock").transform;
            event1.transform.rotation = GameObject.Find("Clock").transform.rotation;
            event1.GetComponent<LongTermEvent>().start = FIVE_start;
            event1.GetComponent<LongTermEvent>().stop = FIVE_stop;
            event1.GetComponent<LongTermEvent>().category += (float)radius / 50f;
            text1 = event1.transform.GetChild(0).gameObject;
            text1.GetComponent<TextLongFollowObject>().start = FIVE_start;
            text1.GetComponent<TextLongFollowObject>().stop = FIVE_stop;
            GameObject event2 = Instantiate(GameObject.Find("OrbitEvent"));
            event2.GetComponent<OrbitEvent>().start = FIVE_start;
            event2.GetComponent<OrbitEvent>().stop = FIVE_stop;
            GameObject text2 = event2.transform.GetChild(0).gameObject;
            text2.GetComponent<OrbitTextObject>().start = FIVE_start;
            text2.GetComponent<OrbitTextObject>().stop = FIVE_stop;
            event2.transform.parent = GameObject.Find("Orbit").transform;
            event2.transform.position = GameObject.Find("Orbit").transform.position;
            Renderer rend2 = event2.GetComponent<Renderer>();
            var tempMaterial2 = new Material(rend2.sharedMaterial);
            tempMaterial2.color = color;
            float emission2 = Mathf.PingPong(Time.time, 1.0f);
            Color32 finalColor2 = color * Mathf.LinearToGammaSpace(emission2);
            tempMaterial2.SetColor("_Color", finalColor2);
            tempMaterial2.SetColor("_EmissionColor", finalColor2);
            rend2.sharedMaterial = tempMaterial2;
            text2.GetComponent<TextMesh>().text = name;
            text2.GetComponent<TextMesh>().color = color;
        }
        
    }


    //Create new UI Button for each email
    public async void CreateNewEmail(string id, string threadId, int start, int stop, Color color, string name, string snippet, string body)
    {
        GameObject button;
        GameObject text;
        button = Instantiate(GameObject.Find("EmailObject"));
        button.transform.GetChild(0).gameObject.SetActive(true);
        Debug.Log("CANVAS OF BUTTON:"+button.transform.GetChild(0).gameObject.name);
        button.GetComponent<EmailEventObject>().start = start;
        button.GetComponent<EmailEventObject>().stop = stop;
        button.GetComponent<EmailEventObject>().snippet = snippet;
        button.GetComponent<EmailEventObject>().body = body;
        text = button.transform.GetChild(0).gameObject;
        text.GetComponentInChildren<Text>().text = name;

        button.transform.parent = GameObject.Find("Clock").transform;
        button.transform.position = GameObject.Find("Clock").transform.position;
        button.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().color = color;

        //If another GameObject already exists with the same thread as this one, then simply add this email to it, and add the thread ID to this email for tracking
        if (GameObject.Find("THREAD:" + threadId) != null)
        {
            GameObject.Find("THREAD:" + threadId).GetComponent<EmailThreadManager>().threadedEmails.Add(id);
            button.GetComponent<EmailEventObject>().thread = threadId;
        }
        //If the email has a thread ID different than the email ID, that means it's part of an email chain.
        //If they don't match, and no other thread object already exists, then create a Thread Object, and attach it to this email
        else if (id != threadId && GameObject.Find("THREAD:" + threadId)==null)
        {
            Debug.Log(id+" NEEDS THREAD "+threadId);
            GameObject thread = Instantiate(GameObject.Find("EmailThreadObject"));
            thread.name = "THREAD:"+threadId;
            thread.transform.parent = button.transform;
            thread.transform.position = button.transform.position;
            thread.GetComponent<EmailThreadManager>().threadedEmails.Add(id);
            button.GetComponent<EmailEventObject>().thread = threadId;
        }
        button.name = id;
    }
}
