using System.Threading.Tasks;
using Common.Messages;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace CustomerManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        readonly IPublishEndpoint _publishEndpoint;

        public OrderController(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        [HttpPost]
        public async Task<ActionResult> Post(OrderAdded order)
        {
            await _publishEndpoint.Publish(order);

            return Ok();
        }
    }
}
