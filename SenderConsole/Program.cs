using Common;
using MassTransit;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SenderConsole
{
    public class Program
    {
        public static async Task Main()
        {
            //var busControl = BusConfigurator.ConfigureBus();

            
            var busControl = Bus.Factory.CreateUsingRabbitMq();

            var source = new CancellationTokenSource(TimeSpan.FromSeconds(10));

            await busControl.StartAsync(source.Token);
            try
            {
                do
                {
                    string value = await Task.Run(() =>
                    {
                        Console.WriteLine("Enter message (or quit to exit)");
                        Console.Write("> ");
                        return Console.ReadLine();
                    });

                    if ("q".Equals(value, StringComparison.OrdinalIgnoreCase))
                        break;

                    for (int i = 0; i < 100; i++)
                    {
                        await busControl.Publish<Customer>(new
                        {
                            Name = $"Customer: {i}"
                        });

                        await busControl.Publish<Address>(new
                        {
                            City = $"Living in {i} street"
                        });
                    }
                }
                while (true);
            }
            finally
            {
                await busControl.StopAsync();
            }
        }
    }
}
