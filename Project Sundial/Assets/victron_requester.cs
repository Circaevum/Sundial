using Newtonsoft.Json;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json.Linq;
using Unity.VisualScripting;

public class victron_requester : MonoBehaviour
{
    private string siteID = "221218";
    private string userID = "258214";
    private string idAccessToken = "269501";
    private string token = "3fe7d4019bc03caf93dc7b0082297226bc9cc407e30822f46fa69375f1eee9c2";
    private string bearer = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsImp0aSI6IjYzYzU5MmMyOWU4YjA2NjJjY2RjZTU0YzM2ZmUzNzhhIn0.eyJ1aWQiOiIyNTgyMTQiLCJ0b2tlbl90eXBlIjoiZGVmYXVsdCIsImlzcyI6InZybWFwaS52aWN0cm9uZW5lcmd5LmNvbSIsImF1ZCI6Imh0dHBzOi8vdnJtYXBpLnZpY3Ryb25lbmVyZ3kuY29tLyIsImlhdCI6MTY3Nzk2MDQzNiwiZXhwIjoxNjc4MDQ2ODM2LCJqdGkiOiI2M2M1OTJjMjllOGIwNjYyY2NkY2U1NGMzNmZlMzc4YSJ9.sbEeeK8hvh95Md5DhdTdNPApqS9Q4k9FyHC4PEP-0n8VKsc6b0UH2duni8Nv7M0i_DW278iIZZY7unXU2d7oqWopqtMkC7sS9jn2Ike3so1INRIC7QvNLDuOgO3qUoyw5oFYw854lh4iiAmQ5wA7fBd-R64rFiKXLZKh9ZOmsk8l1BbQlPB3z-6P7YbSIUfMmenZKoEkVsvxO-M7eLhmARwxY4CQlE78RYCjikxvuxsR7ezC96B_wNg86CYiIpQ4aI2-AU--lQtI8Z8Qe9XQ70ep2KcVgcyKwhr2mN8uQRxsTqyhshgMdMac4rqziNYqGKFtK7Mh1kScpBS7bKPnMQ";
    private string apiURL = "https://vrmapi.victronenergy.com/v2/installations/";
    private string victron_response;
    public GameObject DataLine;
    private string[] metrics = { "solar_yield", "kwh", "consumption" };

    // Start is called before the first frame update
    private void Start()
    {
        string startDate = DateTime.Now.ToString("yyyy-MM-ddT00:00:00");
        //string startDate = DateTime.Now.AddDays(-1).ToString("yyyy-MM-ddT00:00:00");
        string endDate = DateTime.Now.ToString("yyyy-MM-ddT23:59:59");

        foreach (string type in metrics)
        {
            RequestData(-10,type);
        }
        
    }
    void RequestData(int days, string type)
    {
        string interval = "15mins";
        DateTime day = DateTime.UtcNow.AddDays(days);
        DateTime unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        long unixTimestampSeconds = (long)(day - unixEpoch).TotalSeconds;
        print("fff:" + unixTimestampSeconds);
        Root iStats = new Root();

        string url = apiURL + siteID + "/stats?start=" + unixTimestampSeconds + "&type="+type+"&interval=" + interval;
        string myResponse = null;

        StartCoroutine(MakeWebRequest(url, (response) =>
        {
            myResponse = response.ToString();
            iStats = JsonConvert.DeserializeObject<Root>(myResponse);
            List<string> used_properties = new List<string>();
            foreach (var property in iStats.records.GetType().GetProperties())
            {
                if(property.GetValue(iStats.records) is List<List<double>> array && array != null && !used_properties.Contains(property.Name))
                {
                    used_properties.Add(property.Name);
                    List<Tuple<long, float>> responseData = ConvertToListOfTuples(array);
                    GameObject newDataLine = Instantiate(DataLine);
                    newDataLine.SetActive(true);
                    newDataLine.name = day.ToString() + "_" + type+"_"+property.Name;
                    newDataLine.transform.parent = GameObject.Find("Clock").transform;
                    newDataLine.GetComponent<Victron_kWh>().metric = responseData;
                    LineRenderer lineRenderer = newDataLine.GetComponent<LineRenderer>();
                    switch (property.Name)
                    {
                        case "Pb":
                            lineRenderer.startColor = Color.yellow;
                            lineRenderer.endColor = Color.yellow;
                            break;
                        case "Bc":
                            lineRenderer.startColor = Color.green;
                            lineRenderer.endColor = Color.green;
                            break;
                        case "Pc":
                            lineRenderer.startColor = Color.red;
                            lineRenderer.endColor = Color.red;
                            break;
                        case "kwh":
                            lineRenderer.startColor = Color.blue;
                            lineRenderer.endColor = Color.blue;
                            break;
                        default:
                            lineRenderer.startColor = Color.white;
                            lineRenderer.endColor = Color.white;
                            break;
                    }
                }
            }
            //List<Tuple<long, float>> responseData = ConvertToListOfTuples(iStats.records.Pb);
            //foreach ()
            
        }));
    }


    public IEnumerator MakeWebRequest(string url, Action<string> callback)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("x-authorization", "Bearer " + bearer);

            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("BBB:" + request.error);
                callback(null);
            }
            else
            {
                string response = request.downloadHandler.text;
                callback(response);
            }
        }
    }
    public List<Tuple<long, float>> ConvertToListOfTuples(List<List<double>> listOfLists)
    {
        List<Tuple<long, float>> resultList = new List<Tuple<long, float>>();

        foreach (List<double> innerList in listOfLists)
        {
            if (innerList.Count != 2)
            {
                throw new ArgumentException("Each inner list must contain exactly two elements.");
            }
            //print(innerList[0] + ":" + innerList[1]);
            long first = (long)innerList[0];
            float second = (float)innerList[1];

            Tuple<long, float> tuple = new Tuple<long, float>(first, second);
            resultList.Add(tuple);
        }

        return resultList;
    }
}


[System.Serializable]
public class Root
{
    public bool success { get; set; }
    public Records records { get; set; }
    public Totals totals { get; set; }
}
[System.Serializable]
public class Records
{
    public List<List<double>> Bc { get; set; }
    public List<List<double>> Pb { get; set; }
    public List<List<double>> Pc { get; set; }
    public List<List<double>> kwh { get; set; }
}
[System.Serializable]
public class Totals
{
    public double Bc { get; set; }
    public double Pb { get; set; }
    public double Pc { get; set; }
    public double kwh { get; set; }
}
