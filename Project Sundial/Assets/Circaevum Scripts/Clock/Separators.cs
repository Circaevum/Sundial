using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Separators : MonoBehaviour
{
    LineRenderer separators;
    public Transform Earth;
    private Toggle rotateTime;
    private Toggle straight;
    public Transform cam;
    private Sundial sundial;
    // Use this for initialization
    void Start()
    {
        separators = GetComponent<LineRenderer>();
        separators.positionCount = 75;
        sundial = GameObject.Find("Sundial").GetComponent<Sundial>();
        rotateTime = GameObject.Find("Toggle Rotate Time").GetComponent<Toggle>();
        straight = GameObject.Find("Straight Toggle").GetComponent<Toggle>();

    }

    // Update is called once per frame
    void Update()
    {
        if (rotateTime.isOn == true && straight.isOn == false)
        {
            for (int i = 0; i < 75; i++)
            {
                if (i % 3 == 0)
                {
                    separators.SetPosition(i, Earth.position + new Vector3(

                      0,
                      6f / sundial.horizonScale * Mathf.Sin((i * Mathf.PI / 36)- (cam.position.x * Mathf.PI / 180)),
                      -6f / sundial.horizonScale * Mathf.Cos((i * Mathf.PI / 36) - (cam.position.x * Mathf.PI / 180))
                    ));
                }
                else
                {
                    separators.SetPosition(i, Earth.position + new Vector3(
                      0,
                      5f / sundial.horizonScale * Mathf.Sin((i * Mathf.PI / 36)- (cam.position.x * Mathf.PI / 180)),
                      -5f / sundial.horizonScale * Mathf.Cos((i * Mathf.PI / 36) - (cam.position.x * Mathf.PI / 180))
                   ));
                }

            }
        }
        else if (rotateTime.isOn == true && straight.isOn == true)
        {
            for (int i = 0; i < 75; i++)
            {
                if (i % 3 == 0)
                {
                    separators.SetPosition(i, Earth.position + new Vector3(

                      0,
                      6f / sundial.horizonScale * Mathf.Sin(i * Mathf.PI / 36),
                      -6f / sundial.horizonScale * Mathf.Cos(i * Mathf.PI / 36)
                    ));
                }
                else
                {
                    separators.SetPosition(i, Earth.position + new Vector3(
                      0,
                      5f / sundial.horizonScale * Mathf.Sin(i * Mathf.PI / 36),
                      -5f / sundial.horizonScale * Mathf.Cos(i * Mathf.PI / 36)
                   ));
                }

            }
        }
        else if (rotateTime.isOn == false && straight.isOn == false)
        {
            for (int i = 0; i < 75; i++)
            {
                if (i % 3 == 0)
                {
                    separators.SetPosition(i, Earth.position + new Vector3(

                      6f / sundial.horizonScale * Mathf.Sin((i * Mathf.PI / 36) + (cam.position.y * Mathf.PI / 180)),
                      0,
                      -6f / sundial.horizonScale * Mathf.Cos((i * Mathf.PI / 36) + (cam.position.y * Mathf.PI / 180))
                    ));
                }
                else
                {
                    separators.SetPosition(i, Earth.position + new Vector3(
                        5f / sundial.horizonScale * Mathf.Sin((i * Mathf.PI / 36) + (cam.position.y * Mathf.PI / 180)),
                      0,
                      -5f / sundial.horizonScale * Mathf.Cos((i * Mathf.PI / 36) + (cam.position.y * Mathf.PI / 180))
                   ));
                }

            }
        }
        else if (rotateTime.isOn == false && straight.isOn == true)
        {
            for (int i = 0; i < 75; i++)
            {
                if (i % 3 == 0)
                {
                    separators.SetPosition(i, Earth.position + new Vector3(

                      6f / sundial.horizonScale * Mathf.Sin(i * Mathf.PI / 36),
                      0,
                      -6f / sundial.horizonScale * Mathf.Cos(i * Mathf.PI / 36)
                    ));
                }
                else
                {
                    separators.SetPosition(i, Earth.position + new Vector3(
                        5f / sundial.horizonScale * Mathf.Sin(i * Mathf.PI / 36),
                      0,
                      -5f / sundial.horizonScale * Mathf.Cos(i * Mathf.PI / 36)
                   ));
                }

            }
        }

    }
}
