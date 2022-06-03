using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardScreen : MonoBehaviour
{

    [SerializeField] private Text title;
    [SerializeField] private Text title_shadow;
    [SerializeField] private Transform content;
    [SerializeField] private GameObject prefab;

    public void ShowLeaderBoard()
    {
        title.text = "LEADERBOARD - " + GameMaster.instance.difficulty.ToString();
        title_shadow.text = "LEADERBOARD - " + GameMaster.instance.difficulty.ToString();
        #if UNITY_WEBGL
                SetLeaderboard();
        #else
                StartCoroutine(GetLeaderBoard());
        #endif
    }

    IEnumerator GetLeaderBoard()
    {
        int tries = 0;
        while(GameMaster.instance.GetLeaderboard().Count==0 && tries < 60)
        {
            Debug.Log("Waiting for Leaderboard");
            yield return new WaitForSeconds(1.0f);
        }
        
        if(GameMaster.instance.GetLeaderboard().Count > 0)
        {
            SetLeaderboard();
        }
    }

    public float scoreOffset = 38.1f;

    public void ClearLeaderboard()
    {
        if(content != null)
        {
            if(content.childCount > 0)
            {
                foreach (Transform child in content)
                {
                    Destroy(child.gameObject);
                }
            }
        }
    }

    void SetLeaderboard()
    {
        #if UNITY_WEBGL
        List<ThumberWar.Leaderboard.Models.Leaderboard> boardForDiff = new List<ThumberWar.Leaderboard.Models.Leaderboard>()
        {
            new ThumberWar.Leaderboard.Models.Leaderboard()
            {
                 difficulty = ThumberWar.Leaderboard.Models.Enums.DIFFICULTY_LEVEL.EASY,
                 playerName = "Due to limitations the Leaderboard was disabled in the web version of the game"
            }
        };
        #else
        List<ThumberWar.Leaderboard.Models.Leaderboard> leaderboard = GameMaster.instance.GetLeaderboard();

        List<ThumberWar.Leaderboard.Models.Leaderboard> boardForDiff = new List<ThumberWar.Leaderboard.Models.Leaderboard>();
        if (leaderboard.Where(x => x.difficulty == GameMaster.instance.difficulty).Count() > 0)
        {
            if(leaderboard.Where(x => x.difficulty == GameMaster.instance.difficulty).Count() > 10)
            {
                boardForDiff.AddRange(leaderboard.Where(x => x.difficulty == GameMaster.instance.difficulty).OrderBy(x => x.time).Take(10).ToList());
            }
            else
            {
                boardForDiff.AddRange(leaderboard.Where(x => x.difficulty == GameMaster.instance.difficulty).OrderBy(x => x.time).ToList());
            }
            
        }
        #endif
        int currentScoreID = 0;
        
        foreach (var score in boardForDiff)
        {
            GameObject scoreObj = Instantiate(prefab, content, false);
            scoreObj.GetComponent<Score>().SetScore(score.playerName, score.time);
            scoreObj.transform.localPosition = new Vector3(scoreObj.transform.localPosition.x, scoreObj.transform.localPosition.y - (currentScoreID * scoreOffset), scoreObj.transform.localPosition.z);
            currentScoreID++;
        }
    }
}
