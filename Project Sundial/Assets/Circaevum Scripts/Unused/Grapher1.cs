using UnityEngine;

public class Grapher1 : MonoBehaviour
{
    public GameObject Cylinder;
    public float distance = 2000;


    void Start()
    {
        //Vector3 relativePos = target.position - transform.position;
        for (int i = -14400; i < 14400; i++)
            Instantiate(Cylinder, new Vector3(distance * Mathf.Sin(2*Mathf.PI*i/24/60),  i*distance /24/60, distance * Mathf.Cos(2*Mathf.PI * i /24/60)), Quaternion.identity);
    }
}