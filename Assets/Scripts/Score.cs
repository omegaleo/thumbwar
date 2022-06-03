using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] private Text Name;
    [SerializeField] private Text Name_Shadow;

    public void SetScore(string name, TimeSpan time)
    {
        string totalTimeString =
                        string.Format("{0:00}:{1:00}:{2:00}",
                                      (int)(time.TotalHours),
                                      time.Minutes,
                                      time.Seconds);

        Name.text = name + " - " + totalTimeString;
        Name_Shadow.text = name + " - " + totalTimeString;
    }
}
