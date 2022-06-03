using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class UIStyling : MonoBehaviour
{
    [SerializeField] private Font font;
    [SerializeField] private Color mainTextColor;

    void Update()
    {
        if(font != null && mainTextColor != null)
        {
            GameObject[] uiComponents = GameObject.FindGameObjectsWithTag("UI");
            foreach(GameObject component in uiComponents)
            {
                if(component.GetComponent<Text>() != null)
                {
                    component.GetComponent<Text>().font = font;
                    component.GetComponent<Text>().color = mainTextColor;
                }
            }
        }
    }
}
