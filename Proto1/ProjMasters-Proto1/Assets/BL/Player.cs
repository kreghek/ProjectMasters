namespace Assets.BL
{
    public static class Player
    {
        public const int DAY_COUNTER_BASE = 24;
        public const float DECISION_COUNTER_BASE = 120;
        private const int START_MONEY = 1000;
        private const int START_AUTHORITY = 25;

        public static int Money { get; set; } = START_MONEY;
        public static int Autority { get; set; } = START_AUTHORITY;
        public static int FailureCount { get; set; }

        public static int DayNumber { get; set; }
        public static float DayCounter { get; set; } = DAY_COUNTER_BASE;

        public static Decision WaitForDecision { get; set; }

        public static bool WaitTutorial { get; set; }

        public static float DecisionCounter { get; set; } = DECISION_COUNTER_BASE;
    }
}
