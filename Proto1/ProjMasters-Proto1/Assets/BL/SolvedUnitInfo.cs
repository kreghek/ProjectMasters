namespace Assets.BL
{
    public class SolvedUnitInfo
    {
        public SolvedUnitInfo()
        {
            Day = Player.DayNumber;
        }

        public float Cost { get; set; }
        public float TimeLog { get; set; }
        public int Day { get; set; }
    }
}
