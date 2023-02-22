using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EmailEventObject : MonoBehaviour
{
    private Sundial sundial;
    private Button emailButton;
    private GameObject clock;
    private GameObject ParentObject;
    private Slider mainSlider;
    public string thread;
    public string snippet;
    public string body;
    public float radius;
    public int start;
    public int stop;
    public float time;
    private float inc = 12;

    void Start()
    {
        sundial = GameObject.Find("Sundial").GetComponent<Sundial>();
        ParentObject = GameObject.Find("Clock");
        emailButton = GetComponentInChildren<Button>();
        mainSlider = GameObject.Find("Slider").GetComponent<Slider>();
        //radius = ParentObject.GetComponent<ClockValues>().radius+1;
        float angle = start % 288 * 360 / 288;
        if (angle < 180)
            angle += 180;
        transform.eulerAngles = new Vector3(ParentObject.transform.parent.transform.eulerAngles.x, 0, -90 - angle);
    }

    void Update()
    {
        radius = 0.75f * ParentObject.transform.parent.transform.localScale.x + 1;
        time = sundial.now;
        float k = ((start) + (stop - start) / 2f);
        //Every single increment is 5 minutes. So if 0 = Jan 1, 2020, then 144 = 12pm
        //Seemed like a reasonable place to stop as far as resolution considerations
        //5 mins is a reasonable block to schedule down to.
        if (Mathf.Abs((start - time) /240f / inc * mainSlider.value / 4) <= 5)
        {
            if (gameObject.transform.GetChild(0).gameObject.activeSelf == false)
                gameObject.transform.GetChild(0).gameObject.SetActive(true);

            //Button objects can't just rotate with the parent object for some reason,
            //so this code recalculates the positioning every frame, so it's always up-to-date relative to the parent
            transform.position = ParentObject.transform.position +
                Mathf.Cos(GameObject.Find("Circa").transform.rotation.x * 2) * new Vector3(
                    (radius - 0.1f) * Mathf.Sin(Mathf.PI * (2f * start / 24f / inc) - Mathf.PI),
                    (radius - 0.1f) * Mathf.Cos(Mathf.PI * (2f * start / 24f / inc) - Mathf.PI),
                    ((start - time) / 240f / inc * mainSlider.value / 4)) -
                Mathf.Sin(GameObject.Find("Circa").transform.rotation.x * 2) * new Vector3(
                    (radius - 0.1f) * Mathf.Sin(Mathf.PI * (2f * start / 24f / inc) - Mathf.PI),
                    ((start - time) / 240f / inc * mainSlider.value / 4),
                    -(radius - 0.1f) * Mathf.Cos(Mathf.PI * (2f * start / 24f / inc) - Mathf.PI)
                );
        }
        else
        {
            if (gameObject.transform.GetChild(0).gameObject.activeSelf == true)
                gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
        transform.localScale = 0.025f*ParentObject.transform.parent.transform.localScale;
        transform.eulerAngles = new Vector3(
            Camera.main.transform.eulerAngles.x,
            Camera.main.transform.eulerAngles.y,
            0
        );

    }
    public void Email_Clicked()
    {
        //If the left trigger is engaged while an email is clicked, cue it for deletion
        /*if (OVRInput.Get(OVRInput.RawAxis1D.LIndexTrigger) > 0.5f)
        {
            GetComponent<AudioSource>().Play();
            GameObject.Find("Canvas-EmailDeletions").GetComponent<EmailDeletionManager>().CueEmailForDestruction(name);
            emailButton.image.color = new Color(1,0.5f,0.5f,0.3f);
        }
        
        //Otherwise, open up the email
        else
        {
            FillBody();
        }
        */
    }
    public void FillBody()
    {
        GameObject browser = GameObject.Find("EmailWebview");

        //This big loop undoes the highlights from the last selection
        if (browser.transform.parent)
        {
            //Button color back to white (unless it's cued for deletion)
            if(browser.transform.parent.GetComponentInChildren<Button>().image.color== Color.yellow)
                browser.transform.parent.GetComponentInChildren<Button>().image.color = Color.white;

            //Check if the email selected is part of a thread
            if (browser.transform.parent.GetComponentInChildren<EmailEventObject>().thread != "")
            {
                //Find the Thread object associated with this email, and tell it to hide its lines
                GameObject.Find("THREAD:"+browser.transform.parent.GetComponentInChildren<EmailEventObject>().thread).GetComponentInChildren<EmailThreadManager>().HideThread();
            }
        }

        //Highlight this email Yellow
        GetComponentInChildren<Button>().image.color = Color.yellow;

        //Illuminate thread of this email if one exists
        if (thread!= "")
        {
            GameObject.Find("THREAD:" + thread).GetComponent<EmailThreadManager>().RevealThread();
        }

        //Add short version of email body to URL panel in case of HTML errors
        GameObject.Find("URL").GetComponent<TMP_Text>().text = snippet;

        //Relocate browser to selected email, and load email body into HTML of browser
        browser.transform.parent = gameObject.transform;
        browser.transform.position = transform.position + new Vector3(0.5f,0.3f,-0.1f);
        //browser.GetComponent<WebViewPrefab>().WebView.LoadHtml(@body);
    }
}
