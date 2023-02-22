using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;


public class SmartMeter : MonoBehaviour
{
    public List<MeterFeed> points;
    // Start is called before the first frame update
    void Start()
    {
        using (StreamReader r = new StreamReader("./Assets/Data/01.json"))
             {
                 string json = r.ReadToEnd();
                 points = JsonConvert.DeserializeObject<List<MeterFeed>>(json);
             }
        foreach (MeterFeed feeds in points)
        {
            //foreach (float[] pairs in feeds.datapoints)
            //    print(pairs[0]+"     "+pairs[1]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[System.Serializable]
public class MeterFeed
{
    public string target;
    public float[][] datapoints;
}
