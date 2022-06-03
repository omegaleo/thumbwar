using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    public float currentCount = 10;
    // Update is called once per frame
    void Update()
    {
        if(GetComponent<Text>() != null)
        {
            Text lbl = GetComponent<Text>();

            if(GameMaster.instance.showCountdown)
            {
                lbl.enabled = true;
                if (Mathf.Round(currentCount) > 0)
                    currentCount -= Time.deltaTime;

                lbl.text = Mathf.Round(currentCount).ToString();

                if (Mathf.Round(currentCount) == 0)
                {
                    GameMaster.instance.EndFight();
                    lbl.enabled = false;
                }
            }
            else
            {
                currentCount = 10;
                lbl.enabled = false;
            }
        }
    }
}
