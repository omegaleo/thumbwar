using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Victory : MonoBehaviour
{
    [SerializeField] private Text timeText;
    [SerializeField] private Text timeTextShadow;
    [SerializeField] private TimeSpan totalTime;
    [SerializeField] private InputField name;

    bool focusSet = false;

    public void SetTime(TimeSpan time)
    {
        if(timeText != null && timeTextShadow != null)
        {
            string totalTimeString =
                        string.Format("{0:00}:{1:00}:{2:00}",
                                      (int)(time.TotalHours),
                                      time.Minutes,
                                      time.Seconds);

            timeText.text = totalTimeString;
            timeTextShadow.text = totalTimeString;
            totalTime = time;
        }
    }

    private void Update()
    {
        if(!focusSet)
        {
            if(EventSystem.current.currentSelectedGameObject != name.gameObject)
            {
                EventSystem.current.SetSelectedGameObject(name.gameObject);
            }

            focusSet = true;
        }
    }

    public void Submit()
    {
        if(!string.IsNullOrEmpty(name.text))
        {
            GameMaster.instance.SubmitToLeaderboard(new ThumberWar.Leaderboard.Models.Leaderboard
            {
                difficulty = GameMaster.instance.difficulty,
                playerName = name.text,
                time = totalTime
            });
            GameMaster.instance.ShowLeaderboard();
        }
    }
}
