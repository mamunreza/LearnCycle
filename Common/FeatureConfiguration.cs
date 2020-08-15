namespace Common
{
    // should be in common project
    public class FeatureConfiguration<T> where T : IFeatureOptions
    {
        //public MongoDbConfiguration MongoDbConfiguration { get; set; }
        public RabbitMqConfiguration RabbitMqConfiguration { get; set; }
        public T FeatureOptions { get; set; }
    }

    public interface IFeatureOptions
    {
        bool IsActive { get; set; }
    }
}
