namespace Assets.BL
{
    public class Player
    {
        public const int DAY_COUNTER_BASE = 240;
        private const int START_AUTHORITY = 100;
        private const int START_MONEY = 1000;

        public ProjectUnitBase SelectedUnit;

        public Decision[] ActiveDecisions { get; set; }
        public int Autority { get; set; } = START_AUTHORITY;
        public float DayCounter { get; set; } = DAY_COUNTER_BASE;

        public int DayNumber { get; set; }

        public int DeadlineDayNumber { get; set; } = 24;
        public int FailureCount { get; set; }

        public int Money { get; set; } = START_MONEY;
        public int ProjectLevel { get; set; }

        public Decision WaitForDecision { get; set; }

        public bool WaitKeyDayReport { get; set; }

        public bool WaitTutorial { get; set; }
    }
}