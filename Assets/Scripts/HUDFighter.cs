using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static GameMaster;
using static ThumberWar.Leaderboard.Models.Enums;

public class HUDFighter : MonoBehaviour
{

    [SerializeField] private FIGHTER fighter;

    [Header("Components")]
    [SerializeField] private Text lblOpponentName;
    [SerializeField] private Text lblOpponentShadow;

    [SerializeField] private Text victoriesText;
    [SerializeField] private Text victoriesTextShadow;

    [SerializeField] private Text pointsText;
    [SerializeField] private Text pointsTextShadow;

    [SerializeField] private Image player;
    [SerializeField] private Image playerWinning;
    [SerializeField] private Image opponent;
    [SerializeField] private Image opponentWinning;

    private void Update()
    {
        if(GameMaster.instance != null)
        {
            switch(GameMaster.instance.GetWinningSide())
            {
                case FIGHTER.OPPONENT:
                    if(opponentWinning != null)
                        opponentWinning.enabled = true;
                    if(playerWinning != null)
                        playerWinning.enabled = false;
                    if(player != null)
                        player.enabled = false;
                    if (opponent != null)
                        opponent.enabled = false;
                    break;
                case FIGHTER.PLAYER:
                    if (opponentWinning != null)
                        opponentWinning.enabled = false;
                    if (playerWinning != null)
                        playerWinning.enabled = true;
                    if (player != null)
                        player.enabled = false;
                    if (opponent != null)
                        opponent.enabled = false;
                    break;
                default:
                    if (opponentWinning != null)
                        opponentWinning.enabled = false;
                    if (playerWinning != null)
                        playerWinning.enabled = false;
                    if (player != null)
                        player.enabled = true;
                    if (opponent != null)
                        opponent.enabled = true;
                    break;
            }

            if (lblOpponentName != null && lblOpponentShadow != null)
            {
                lblOpponentName.text = "OPPONENT " + GameMaster.instance.level;
                lblOpponentShadow.text = "OPPONENT " + GameMaster.instance.level;
            }

            if (victoriesText != null && victoriesTextShadow != null)
            {
                string victories = GameMaster.instance.GetVictoriesTextFormatted(fighter);
                victoriesText.text = victories;
                victoriesTextShadow.text = victories;
            }

            if (pointsText != null && pointsTextShadow != null)
            {
                string points = GameMaster.instance.GetPoints(fighter).ToString();
                pointsText.text = points;
                pointsTextShadow.text = points;
            }
        }
    }
}
