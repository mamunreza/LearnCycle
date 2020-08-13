using MassTransit;
using MassTransit.RabbitMqTransport;
using System;
using System.Collections.Generic;
using System.Text;

namespace SenderConsole
{
    public static class BusConfigurator
    {
        public static IBusControl ConfigureBus(
            Action<IRabbitMqBusFactoryConfigurator> configureAction = null)
        {
            return Bus.Factory.CreateUsingRabbitMq(configure =>
            {
                configure.ConfigureRabbitMq();
                configureAction?.Invoke(configure);
            });
        }

        public static void ConfigureRabbitMq(this IRabbitMqBusFactoryConfigurator configure)
        {
            // TODO: Configure redelivery 
            var host = ConfigureHost(configure);

            //ConfigureMessageScheduler(configure, host, rabbitMqConfiguration);
            configure.PrefetchCount = 8;
        }

        private static IRabbitMqHost ConfigureHost(IRabbitMqBusFactoryConfigurator configure)
        {
            var virtualHost = string.IsNullOrEmpty(RabbitMqConstant.VirtualHost) ? "/" : RabbitMqConstant.VirtualHost;
            var host = configure.Host(RabbitMqConstant.HostName, RabbitMqConstant.Port, virtualHost, c =>
            {
                c.Username(RabbitMqConstant.UserName);
                c.Password(RabbitMqConstant.Password);
               
            });
            return host;
        }   
    }
}
