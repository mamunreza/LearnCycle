using Common.Messages;
using MassTransit;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace CustomerManagement.Consumers
{
    // should be in feature project
    public class OrderAddedConsumer :
        IConsumer<OrderAdded>
    {
        ILogger<OrderAddedConsumer> _logger;

        public OrderAddedConsumer(ILogger<OrderAddedConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<OrderAdded> context)
        {
            //_logger.LogInformation("Message: {Value}", context.Message.Id);
            await Task.CompletedTask;
        }
    }
}
