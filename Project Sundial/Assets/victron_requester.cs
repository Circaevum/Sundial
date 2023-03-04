using Newtonsoft.Json;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class victron_requester : MonoBehaviour
{
    private string siteID = "221218";
    private string userID = "258214";
    private string idAccessToken = "269501";
    private string token = "3fe7d4019bc03caf93dc7b0082297226bc9cc407e30822f46fa69375f1eee9c2";
    private string bearer = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsImp0aSI6IjYzYzU5MmMyOWU4YjA2NjJjY2RjZTU0YzM2ZmUzNzhhIn0.eyJ1aWQiOiIyNTgyMTQiLCJ0b2tlbl90eXBlIjoiZGVmYXVsdCIsImlzcyI6InZybWFwaS52aWN0cm9uZW5lcmd5LmNvbSIsImF1ZCI6Imh0dHBzOi8vdnJtYXBpLnZpY3Ryb25lbmVyZ3kuY29tLyIsImlhdCI6MTY3Nzk2MDQzNiwiZXhwIjoxNjc4MDQ2ODM2LCJqdGkiOiI2M2M1OTJjMjllOGIwNjYyY2NkY2U1NGMzNmZlMzc4YSJ9.sbEeeK8hvh95Md5DhdTdNPApqS9Q4k9FyHC4PEP-0n8VKsc6b0UH2duni8Nv7M0i_DW278iIZZY7unXU2d7oqWopqtMkC7sS9jn2Ike3so1INRIC7QvNLDuOgO3qUoyw5oFYw854lh4iiAmQ5wA7fBd-R64rFiKXLZKh9ZOmsk8l1BbQlPB3z-6P7YbSIUfMmenZKoEkVsvxO-M7eLhmARwxY4CQlE78RYCjikxvuxsR7ezC96B_wNg86CYiIpQ4aI2-AU--lQtI8Z8Qe9XQ70ep2KcVgcyKwhr2mN8uQRxsTqyhshgMdMac4rqziNYqGKFtK7Mh1kScpBS7bKPnMQ";
    private string apiURL = "https://vrmapi.victronenergy.com/v2/installations/";
    private string victron_response;
    public GameObject kWh_event;

    // Start is called before the first frame update
    private void Start()
    {
        string startDate = DateTime.Now.ToString("yyyy-MM-ddT00:00:00");
        //string startDate = DateTime.Now.AddDays(-1).ToString("yyyy-MM-ddT00:00:00");
        string endDate = DateTime.Now.ToString("yyyy-MM-ddT23:59:59");

        for (int i = 0; i <= 10; i++)
            RequestData(-i);
        //return iStats;
    }
    void RequestData(int days)
    {
        string interval = "15mins";
        DateTime day = DateTime.UtcNow.AddDays(days);
        DateTime unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        long unixTimestampSeconds = (long)(day - unixEpoch).TotalSeconds;
        print("fff:" + unixTimestampSeconds);
        Root iStats = new Root();

        string url = apiURL + siteID + "/stats?start=" + unixTimestampSeconds + "&type=solar_yield&interval=" + interval;
        string myResponse = null;

        StartCoroutine(MakeWebRequest(url, (response) =>
        {
            myResponse = response.ToString();
            iStats = JsonConvert.DeserializeObject<Root>(myResponse);
            List<Tuple<long, float>> battery_cap = ConvertToListOfTuples(iStats.records.Pb);
            print(battery_cap.First().Item1);
            GameObject newKwh = Instantiate(kWh_event);
            newKwh.SetActive(true);
            newKwh.transform.parent = GameObject.Find("Clock").transform;
            newKwh.GetComponent<Victron_kWh>().metric = battery_cap;
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
