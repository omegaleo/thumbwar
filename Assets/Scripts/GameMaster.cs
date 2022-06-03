using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static GeneralControls;
using static ThumberWar.Leaderboard.Models.Enums;

public class GameMaster : MonoBehaviour
{
    [SerializeField]
    private Dictionary<DIFFICULTY_LEVEL, float> percentageScalingPerDifficulty = new Dictionary<DIFFICULTY_LEVEL, float>()
    {
        { DIFFICULTY_LEVEL.EASY, 0.05f }, //0.33% to hit and goes 0.33%, 0.66%, 0.99%, 1.32%
        { DIFFICULTY_LEVEL.NORMAL, 0.125f }, // 0.75, 1.5, 2.25, 3
        { DIFFICULTY_LEVEL.HARD, 0.18f } // 1.5, 3, 4.5, 6
    }; //per tick

    public static GameMaster instance;

    [Header("Components")]
    //Handle difficulty
    public DIFFICULTY_LEVEL difficulty;
    [SerializeField] private Leaderboard leaderboard;
    [SerializeField] private GameObject victoryScreen;
    [SerializeField] private GameObject leaderBoardScreen;
    [SerializeField] private GameObject defeatScreen;
    [SerializeField] private GameObject optionsScreen;

    [Header("Audio")]
    [SerializeField] public AudioSource bgm;
    [SerializeField] private AudioClip music;
    [SerializeField] private AudioClip defeat;
    [SerializeField] private AudioClip victory;
    public AudioSource sfx;
    public AudioClip btnClick;

    [Header("Values")]
    public int playerVictories = 0;
    public int playerPoints = 0;
    public int opponentVictories = 0;
    public int opponentPoints = 0;
    public string victoryCharacter = "\u25CF";
    public int level = 1;
    public bool isPlaying = false;
    [SerializeField] private DateTime startingTime;
    [SerializeField] private DateTime endTime;

    [Header("Scenes")]
    public int NewGameScene = 1;
    public int CreditsScene = 2;
    public int OptionsScene = 3;
    
    public bool showCountdown;

    public void SubmitToLeaderboard(ThumberWar.Leaderboard.Models.Leaderboard leaderboardEntry)
    {
        leaderboard.Insert(leaderboardEntry);
        leaderboard.UpdateLeaderboard();
    }

    public List<ThumberWar.Leaderboard.Models.Leaderboard> GetLeaderboard()
    {
        return leaderboard.leaderboards;
    }

    public void ShowLeaderboard()
    {
        victoryScreen.SetActive(false);
        leaderBoardScreen.SetActive(true);
        LeaderboardScreen screen = leaderBoardScreen.GetComponent<LeaderboardScreen>();
        screen.ShowLeaderBoard();
    }

    public void ToggleOptions()
    {
        if(optionsScreen != null)
        {
            optionsScreen.SetActive(!optionsScreen.activeSelf);
        }
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        if(playerPoints > opponentPoints || opponentPoints > playerPoints)
        {
            showCountdown = true;
        }
        else
        {
            showCountdown = false;
        }

        if (CanContinue())
        {
            EnemyCalculatePoints();
        }
    }

    public bool btnPressed = false;
    float mashAmount = 0.0f;
    public void MashButton()
    {
        if(CanContinue())
        {
            btnPressed = true;
            mashAmount += 0.25f;
            if(mashAmount == 1.0f)
            {
                playerPoints += 1;
                mashAmount = 0.0f;
            }
        }
    }

    public void AnyKey()
    {
        if (defeatScreen != null)
        {
            if (defeatScreen.activeSelf)
            {
                SceneManager.LoadScene(0);
                defeatScreen.SetActive(false);
            }
        }
        if (leaderBoardScreen != null)
        {
            if(leaderBoardScreen.activeSelf)
            {
                SceneManager.LoadScene(0);
                leaderBoardScreen.SetActive(false);
                LeaderboardScreen screen = leaderBoardScreen.GetComponent<LeaderboardScreen>();
                screen.ClearLeaderboard();
            }
        }
    }

    private void EnemyCalculatePoints()
    {
        float probability = 0.0f;
        percentageScalingPerDifficulty.TryGetValue(difficulty, out probability);

        probability *= (level * 0.5f);

        if(OpponentMash(probability))
        {
            opponentPoints++;
        }
    }

    private bool OpponentMash(float probability)
    {
        return UnityEngine.Random.Range(0.0f, 100.0f) <= probability;
    }

    public string GetVictoriesTextFormatted(FIGHTER fighter)
    {
        string result = "";
        int count = (fighter == FIGHTER.PLAYER)?playerVictories:opponentVictories;
        for(int i = 0; i < count; i++)
        {
            result += victoryCharacter;
        }
        return result;
    }

    public int GetPoints(FIGHTER fighter)
    {
        return (fighter == FIGHTER.PLAYER)?playerPoints:opponentPoints;
    }

    public void StartGame()
    {
        level = 1;
        playerVictories = 0;
        playerPoints = 0;
        opponentVictories = 0;
        opponentPoints = 0;
        startingTime = DateTime.UtcNow;
        isPlaying = true;
    }

    void NextLevel()
    {
        playerVictories = 0;
        playerPoints = 0;
        opponentVictories = 0;
        opponentPoints = 0;
        level++;
        isPlaying = true;
    }

    public void EndFight()
    {
        showCountdown = false;
        isPlaying = false;
        if (playerPoints > opponentPoints)
        {
            opponentPoints = 0;
            playerPoints = 0;
            playerVictories++;
            if(playerVictories == 3)
            {
                if(level < 4)
                {
                    NextLevel();
                }
                if(level == 4)
                {
                    endTime = DateTime.UtcNow;

                    TimeSpan totalTime = (endTime - startingTime);

                    string totalTimeString =
                        string.Format("{0:00}:{1:00}:{2:00}",
                                      (int)(totalTime.TotalHours),
                                      totalTime.Minutes,
                                      totalTime.Seconds);

                    if(bgm != null)
                    {
                        bgm.loop = false;
                        bgm.clip = victory;
                        bgm.Play();
                    }
                    
                    victoryScreen.SetActive(true);
                    Victory victoryController = victoryScreen.GetComponent<Victory>();
                    victoryController.SetTime(totalTime);
                }
            }
        }
        else if(playerPoints < opponentPoints)
        {
            opponentPoints = 0;
            playerPoints = 0;
            opponentVictories++;
            if(opponentVictories == 3)
            {
                if (bgm != null)
                {
                    bgm.loop = false;
                    bgm.clip = defeat;
                    bgm.Play();
                }
                defeatScreen.SetActive(true);
            }
        }
        else
        {
            Debug.Log("This should not happen");
        }

        isPlaying = true;
    }

    public FIGHTER GetWinningSide()
    {
        if(playerPoints > opponentPoints)
            return FIGHTER.PLAYER;
        else if(opponentPoints > playerPoints)
            return FIGHTER.OPPONENT;
        else
            return FIGHTER.NONE;
    }

    bool CanContinue()
    {
        return isPlaying && !defeatScreen.activeSelf && !victoryScreen.activeSelf && !leaderBoardScreen.activeSelf;
    }
}
