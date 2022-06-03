using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventSystemHandler : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(EventSystem.current != null)
        {
            if(EventSystem.current.currentSelectedGameObject == null)
            {
                if(EventSystem.current.firstSelectedGameObject != null)
                {
                    EventSystem.current.SetSelectedGameObject(EventSystem.current.firstSelectedGameObject);
                }
            }
        }
    }
}
