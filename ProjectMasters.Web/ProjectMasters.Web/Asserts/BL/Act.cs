namespace Assets.BL
{
    public sealed class Act
    {
        private const float baseCommitTimeSeconds = 1;

        private float _commitCounter = baseCommitTimeSeconds;

        public ActTargetPattern ActTargetPattern { get; set; }
        public ActImpact Impact { get; set; }

        public bool IsReadyToUse => _commitCounter <= 0;
        public ActPosition Position { get; set; }

        public void Reset()
        {
            _commitCounter = baseCommitTimeSeconds;
        }

        public void Update(float commitDeltaTime)
        {
            if (_commitCounter <= 0)
            {
            }
            else
            {
                _commitCounter -= commitDeltaTime;
            }
        }
    }
}