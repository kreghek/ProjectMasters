using ProjectMasters.Games;

namespace Assets.BL
{
    public class SolvedUnitInfo
    {
        public SolvedUnitInfo(GameState gameState)
        {
            Day = gameState.Player.DayNumber;
        }

        public float Cost { get; set; }
        public int Day { get; set; }
        public float TimeLog { get; set; }
    }
}