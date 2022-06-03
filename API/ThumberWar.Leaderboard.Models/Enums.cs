using System;
using System.Collections.Generic;
using System.Text;

namespace ThumberWar.Leaderboard.Models
{
    [Serializable]
    public static class Enums
    {
        public enum DIFFICULTY_LEVEL
        {
            EASY,
            NORMAL,
            HARD
        }
        public enum FIGHTER { PLAYER, OPPONENT, NONE }
    }
}
