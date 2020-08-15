using Common;
using CustomerManagement.Consumers;
using MassTransit;
using SimpleInjector;

namespace CustomerManagement
{
    // should be in feature project
    public class OrderMessageConsumptionFeature
    {
        public void Activate(Container container, FeatureConfiguration<OrderMessageConsumptionFeatureOptions> featureConfiguration)
        {
            container.RegisterInitializer<IBusControl>(bus => ConfigureBus(bus, container, featureConfiguration));

            container.Register<OrderAddedConsumer>();
        }

        private static void ConfigureBus(IBusControl bus, Container container, FeatureConfiguration<OrderMessageConsumptionFeatureOptions> featureConfiguration)
        {
            var queueName = featureConfiguration.FeatureOptions.QueueName;
            bus.ConnectReceiveEndpoint(queueName, configurator =>
            {
                configurator.Consumer<OrderAddedConsumer>(container);

                //configurator.UseMessageRetry(r => r.Immediate(5));
            });
        }
    }
}
