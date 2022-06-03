using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Option : MonoBehaviour
{
    public enum OPTION_TYPE
    {
        MUSIC,
        SFX,
        DIFFICULTY,
        NONE
    };

    [SerializeField] private OPTION_TYPE type;

    [SerializeField] private GameObject content;
    [SerializeField] private GameObject contentShadow;

    // Update is called once per frame
    void Update()
    {
        if(type == OPTION_TYPE.MUSIC)
        {
            content.GetComponent<Image>().fillAmount = GameMaster.instance.bgm.volume;
        }
        if(type == OPTION_TYPE.SFX)
        {
            content.GetComponent<Image>().fillAmount = GameMaster.instance.sfx.volume;
        }
        else if(type == OPTION_TYPE.DIFFICULTY)
        {
            content.GetComponent<Text>().text = GameMaster.instance.difficulty.ToString();
            contentShadow.GetComponent<Text>().text = GameMaster.instance.difficulty.ToString();
        }
    }

    public void Increase()
    {
        if (type == OPTION_TYPE.MUSIC)
        {
            if(GameMaster.instance.bgm.volume < 1.0f)
            {
                GameMaster.instance.bgm.volume += 0.1f;
            }
        }
        else if (type == OPTION_TYPE.SFX)
        {
            if (GameMaster.instance.sfx.volume < 1.0f)
            {
                GameMaster.instance.sfx.volume += 0.1f;
            }
        }
        else if (type == OPTION_TYPE.DIFFICULTY)
        {
            if(GameMaster.instance.difficulty == ThumberWar.Leaderboard.Models.Enums.DIFFICULTY_LEVEL.EASY)
            {
                GameMaster.instance.difficulty = ThumberWar.Leaderboard.Models.Enums.DIFFICULTY_LEVEL.NORMAL;
            }
            else if (GameMaster.instance.difficulty == ThumberWar.Leaderboard.Models.Enums.DIFFICULTY_LEVEL.NORMAL)
            {
                GameMaster.instance.difficulty = ThumberWar.Leaderboard.Models.Enums.DIFFICULTY_LEVEL.HARD;
            }
            else if (GameMaster.instance.difficulty == ThumberWar.Leaderboard.Models.Enums.DIFFICULTY_LEVEL.HARD)
            {
                GameMaster.instance.difficulty = ThumberWar.Leaderboard.Models.Enums.DIFFICULTY_LEVEL.EASY;
            }
        }
    }

    public void Decrease()
    {
        if (type == OPTION_TYPE.MUSIC)
        {
            if (GameMaster.instance.bgm.volume > 0.0f)
            {
                GameMaster.instance.bgm.volume -= 0.1f;
            }
        }
        else if (type == OPTION_TYPE.SFX)
        {
            if (GameMaster.instance.sfx.volume > 0.0f)
            {
                GameMaster.instance.sfx.volume -= 0.1f;
            }
        }
        else if (type == OPTION_TYPE.DIFFICULTY)
        {
            if (GameMaster.instance.difficulty == ThumberWar.Leaderboard.Models.Enums.DIFFICULTY_LEVEL.EASY)
            {
                GameMaster.instance.difficulty = ThumberWar.Leaderboard.Models.Enums.DIFFICULTY_LEVEL.HARD;
            }
            else if (GameMaster.instance.difficulty == ThumberWar.Leaderboard.Models.Enums.DIFFICULTY_LEVEL.NORMAL)
            {
                GameMaster.instance.difficulty = ThumberWar.Leaderboard.Models.Enums.DIFFICULTY_LEVEL.EASY;
            }
            else if (GameMaster.instance.difficulty == ThumberWar.Leaderboard.Models.Enums.DIFFICULTY_LEVEL.HARD)
            {
                GameMaster.instance.difficulty = ThumberWar.Leaderboard.Models.Enums.DIFFICULTY_LEVEL.NORMAL;
            }
        }
    }

    public void Close()
    {
        GameMaster.instance.ToggleOptions();
    }
}
