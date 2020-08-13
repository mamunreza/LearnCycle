using Common;
using MassTransit;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ReceiverConsole
{
    public class Program
    {
        public static async Task Main()
        {
            var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.ReceiveEndpoint("event-listener-customer", e =>
                {
                    e.Consumer<CustomerConsumer>();
                });
                cfg.ReceiveEndpoint("event-listener-address", e =>
                {
                    e.Consumer<AddressConsumer>();
                });
            });

            var source = new CancellationTokenSource(TimeSpan.FromSeconds(10));

            await busControl.StartAsync(source.Token);
            try
            {
                Console.WriteLine("Press enter to exit");
                await Task.Run(() => Console.ReadLine());
            }
            finally
            {
                await busControl.StopAsync();
            }
        }

        public class CustomerConsumer :
            IConsumer<Customer>
        {
            public async Task Consume(ConsumeContext<Customer> context)
            {
                Console.WriteLine("Value: {0}", context.Message.Name);
                await Task.CompletedTask;
            }
        }
        public class AddressConsumer :
            IConsumer<Address>
        {
            public async Task Consume(ConsumeContext<Address> context)
            {
                Console.WriteLine("Value: {0}", context.Message.City);
                await Task.CompletedTask;
            }
        }
    }
}
