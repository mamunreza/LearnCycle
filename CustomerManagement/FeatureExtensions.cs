using Common;
using CustomerManagement.Consumers;
using MassTransit;
using Microsoft.Extensions.Configuration;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CustomerManagement
{
    public static class FeatureExtensions
    {
        // should be in api
        public static void AddMessageConsumptionFeature(this Container container, IConfiguration configuration)
        {
            var featureOptions = new FeatureConfiguration<MessageConsumptionFeatureOptions>
            {
                //MongoDbConfiguration = configuration.GetSection("Mongo").Get<MongoDbConfiguration>() ?? throw new Exception("MongoDb not configured"),
                FeatureOptions = configuration.GetSection("CustomerDataMessageConsumption").Get<MessageConsumptionFeatureOptions>() ?? throw new Exception("CustomerDataMessageConsumption not configured")
            };

            //// if feature is disabled, this needs to be register anyway
            //container.RegisterSingleton<ICleanupRepository, CleanupRepository>();
            //container.RegisterSingleton<ICustomerCleanupJobService, CustomerCleanupJobService>();

            if (featureOptions.FeatureOptions.IsActive)
            {
                new MessageConsumptionFeature().Activate(container, featureOptions);
            }
        }

        // should be in api
        public static void AddOrderMessageConsumptionFeature(this Container container, IConfiguration configuration)
        {
            var featureOptions = new FeatureConfiguration<OrderMessageConsumptionFeatureOptions>
            {
                FeatureOptions = configuration.GetSection("OrderMessageConsumption").Get<OrderMessageConsumptionFeatureOptions>()
                    ?? throw new Exception("OrderMessageConsumption not configured")
            };

            if (featureOptions.FeatureOptions.IsActive)
            {
                new OrderMessageConsumptionFeature().Activate(container, featureOptions);
            }
        }

        public static void AddRabbitMq(this Container container, IConfiguration configuration)
        {
            var rabbitMqConfiguration = configuration.GetSection("RabbitMq").Get<RabbitMqConfiguration>();
            //container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
            container.AddMassTransit(x =>
            {

                x.AddBus((cntx) =>
                {
                    var bus = Bus.Factory.CreateUsingRabbitMq(rmq =>
                    {
                        rmq.ConfigureRabbitMq(rabbitMqConfiguration);
                        rmq.UseHealthCheck(cntx);
                    });
                    //bus.ConnectConsumeObserver(container.GetInstance<MessageIdRecorder>());
                    //bus.ConnectConsumeObserver(container.GetInstance<ApplicationInsightsRequestRecorder>());
                    return bus;
                });


            });

            //IBusControl busControl = container.GetInstance<IBusControl>();
            //busControl.Start();
        }
    }
}
