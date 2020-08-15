using MassTransit;
using MassTransit.RabbitMqTransport;
using System;
using System.Collections.Generic;
using System.Net.Security;
using System.Text;

namespace Common
{
    // should be in common project
    public static class BusControl
    {
        public static void ConfigureRabbitMq(this IRabbitMqBusFactoryConfigurator configure, RabbitMqConfiguration rabbitMqConfiguration)
        {
            // TODO: Configure redelivery 
            var host = ConfigureHost(rabbitMqConfiguration, configure);

            //ConfigureMessageScheduler(configure, host, rabbitMqConfiguration);
            configure.PrefetchCount = 8;
        }

        private static IRabbitMqHost ConfigureHost(RabbitMqConfiguration rabbitMqConfiguration,
            IRabbitMqBusFactoryConfigurator configure)
        {
            var virtualHost = string.IsNullOrEmpty(rabbitMqConfiguration.VirtualHost) ? "/" : rabbitMqConfiguration.VirtualHost;
            var host = configure.Host(rabbitMqConfiguration.Hostname, rabbitMqConfiguration.Port, virtualHost, c =>
            {
                c.Username(rabbitMqConfiguration.Username);
                c.Password(rabbitMqConfiguration.Password);
                var nodeList = rabbitMqConfiguration.ClusterNodesList;
                if (nodeList.Length > 0)
                {
                    c.UseCluster(x =>
                    {
                        foreach (var node in nodeList)
                        {
                            x.Node(node);
                        }
                    });
                }

                if (rabbitMqConfiguration.IsTlsConnection)
                {
                    c.UseSsl(sc => ConfigureSsl(sc, rabbitMqConfiguration));
                }

                var heartbeat = rabbitMqConfiguration.HeartbeatInterval;
                if (heartbeat > 0)
                {
                    c.Heartbeat((ushort)heartbeat);
                }
            });
            return host;
        }

        //private static void ConfigureMessageScheduler(IRabbitMqBusFactoryConfigurator configure, IRabbitMqHost host, RabbitMqConfiguration rabbitMqConfiguration)
        //{
        //    if (string.IsNullOrWhiteSpace(rabbitMqConfiguration.SchedulerQueue))
        //    {
        //        throw new InvalidOperationException("RabbitMq.SchedulerQueue configuration required");
        //    }
        //    configure.UseMessageScheduler(new Uri(host.Address.ToString() + "/" + rabbitMqConfiguration.SchedulerQueue));
        //}

        private static void ConfigureSsl(IRabbitMqSslConfigurator sc, RabbitMqConfiguration rabbitMqConfiguration)
        {
            if (rabbitMqConfiguration.UseTlsPolicy)
            {
                const SslPolicyErrors policy = SslPolicyErrors.RemoteCertificateChainErrors |
                                               SslPolicyErrors.RemoteCertificateNameMismatch |
                                               SslPolicyErrors.RemoteCertificateNotAvailable;
                sc.AllowPolicyErrors(policy);
            }
            sc.ServerName = rabbitMqConfiguration.Hostname;
            sc.Protocol = System.Security.Authentication.SslProtocols.Tls12;
            sc.UseCertificateAsAuthenticationIdentity = false;
        }
    }
}
