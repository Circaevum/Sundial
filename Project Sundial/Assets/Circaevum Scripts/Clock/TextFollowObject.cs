using UnityEngine;
using UnityEngine.UI;

public class TextFollowObject : MonoBehaviour {
    private Sundial sundial;
    private GameObject clock;
    private Slider mainSlider;
    public float radius;
    public int start;
    public int stop;
    public int parentLineCount;
    public float time;
    public float extInc;
    private float inc = 12;
    // Use this for initialization
    void Start()
    {
        sundial = GameObject.Find("Sundial").GetComponent<Sundial>();
        float k = start + ((float)stop - (float)start) / 2f;
        mainSlider = GameObject.Find("Slider").GetComponent<Slider>();
        clock = transform.parent.transform.gameObject;
        parentLineCount = transform.parent.GetComponent<LineRenderer>().positionCount;
    }
    void Update()
    {
        if(GameObject.Find("Zoom Toggle").GetComponent<Toggle>().isOn)
        {
            extInc = 24f;
        }else{
            extInc = 240f;
        }
        time = sundial.now;
        float k = ((start) + (stop - start) / 2f);
        GetComponent<TextMesh>().characterSize=1;
        float scale = clock.transform.localScale.x/2*1000;
        transform.eulerAngles = transform.parent.transform.eulerAngles;
        transform.position = transform.parent.transform.position  +
            new Vector3(
                -(radius - 0.01f) * scale * Mathf.Sin(Mathf.PI * (2f * k / 24f / inc) - 3*Mathf.PI/4),
                0,
                (radius - 0.01f) * scale * Mathf.Cos(Mathf.PI * (2f * k / 24f / inc) - 3*Mathf.PI/4)
            );
        //Debug.Log("COS("+ GameObject.Find("Circa").transform.rotation.x*2+") = "+ Mathf.Cos(GameObject.Find("Circa").transform.rotation.x * 2));
        transform.LookAt(Camera.main.transform);
        
        
    }
}
