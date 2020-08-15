using Common.Messages;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerManagement.Consumers
{
    public class CustomerAddedConsumer :
        IConsumer<CustomerAdded>
    {
        ILogger<CustomerAddedConsumer> _logger;

        public CustomerAddedConsumer(ILogger<CustomerAddedConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<CustomerAdded> context)
        {
            //_logger.LogInformation("Message: {Value}", context.Message.Id);
            await Task.CompletedTask;
        }
    }

    //public class CustomerRefineConsumer :
    //   IConsumer<CustomerAdded>
    //{
    //    ILogger<CustomerRefineConsumer> _logger;

    //    public CustomerRefineConsumer(ILogger<CustomerRefineConsumer> logger)
    //    {
    //        _logger = logger;
    //    }

    //    public async Task Consume(ConsumeContext<CustomerAdded> context)
    //    {
    //        //_logger.LogInformation("Message: {Value}", context.Message.Id);
    //        await Task.CompletedTask;
    //    }
    //}
}
