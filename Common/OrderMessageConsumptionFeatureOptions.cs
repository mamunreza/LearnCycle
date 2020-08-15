namespace Common
{
    // should be in feature project
    public class OrderMessageConsumptionFeatureOptions : IFeatureOptions
    {
        public bool IsActive { get; set; }
        public string QueueName { get; set; }
        public bool IsCleanupActive { get; set; }
        public int CleanupPeriodInSeconds { get; set; }
        public RedeliverySettings Redelivery { get; set; }
    }
}
