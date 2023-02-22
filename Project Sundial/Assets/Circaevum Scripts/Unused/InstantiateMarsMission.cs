using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstantiateMarsMission : MonoBehaviour
{
    public GameObject Earth2Mars;

    void Start()
    {

    }
    public void InstantiateMarsButtonClicked()
    {
        CreateMarsMission(10800, 84240, Color.red);
    }
    public void CreateMarsMission(int tStart, int tStop, Color color1)
    {
        Earth2Mars = GameObject.Find("Earth2Mars");
        GameObject mission1 = Instantiate(Earth2Mars);

        Renderer rend = mission1.GetComponent<Renderer>();
        rend.material.color = color1;
        rend.material.SetColor("_EmissionColor", color1);
        //text1.

        //Material mat1 = new Material(shader);
        //mat1.color = color1;
        //event1.SetActive(true);
        mission1.GetComponent<Earth2Mars>().start = tStart;
        mission1.GetComponent<Earth2Mars>().stop = tStop;
        //event1.GetComponent<StraightEventModeler>().GetComponent<Material>().color = Color.red;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
