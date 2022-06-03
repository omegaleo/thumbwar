using System;
using static ThumberWar.Leaderboard.Models.Enums;

namespace ThumberWar.Leaderboard.Models
{
    public class Leaderboard
    {
        public DIFFICULTY_LEVEL difficulty;
        public string playerName;
        public TimeSpan time;
    }
}
