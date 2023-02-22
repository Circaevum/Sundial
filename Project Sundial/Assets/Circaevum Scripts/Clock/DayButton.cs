using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class DayButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI text;
    public GameObject pointer;
    private bool isHoveringObject;

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHoveringObject = true;
        transform.localScale = new Vector3(-0.03f, 0.03f, 0.03f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHoveringObject = false;
        transform.localScale = new Vector3(-0.003f, 0.003f, 0.003f);
    }

    private void Update()
    {
        if (isHoveringObject)
        {
            int newInt = TMP_TextUtilities.FindIntersectingWord(GetComponent<TMP_Text>(), pointer.transform.position, Camera.main);
            Debug.Log(newInt);
        }
        if (Input.GetMouseButtonDown(0))
        {
            transform.localScale = new Vector3(-0.03f, 0.03f, 0.03f);
        }
    }

    
}
