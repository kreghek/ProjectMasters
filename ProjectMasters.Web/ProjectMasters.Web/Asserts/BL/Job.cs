namespace Assets.BL
{
    public class Job
    {
        public int CompleteErrorsAmount { get; set; }
        public int CompleteSubTasksAmount { get; set; }
        public int FeatureDecomposesAmount { get; set; }
        public JobScheme Scheme { get; set; }
    }
}