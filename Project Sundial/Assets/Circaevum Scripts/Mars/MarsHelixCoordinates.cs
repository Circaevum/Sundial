using UnityEngine;
using System.Collections;

public class MarsHelixCoordinates : MonoBehaviour
{
    private Sundial sundial;
    public Transform cam;
    // Use this for initialization
    void Start()
    {
        sundial = GameObject.Find("Sundial").GetComponent<Sundial>();
    }

    // Update is called once per frame
    void Update()
    {
        //Update orbital ring to follow camera on y-axis
        transform.position = new Vector3(sundial.distances[3] * Mathf.Sin((cam.position.y) / 24 / 697 * Mathf.PI / 180f), 0, sundial.distances[3] - (sundial.distances[3] * Mathf.Cos((cam.position.y) / 24 / 697 * Mathf.PI / 180f)));
        transform.rotation = Quaternion.Euler(0, +cam.position.y, 0);
        //Closest to tile were (-90,0,0) followed by (-67,90,-90)
    }
}