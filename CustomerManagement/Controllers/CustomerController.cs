using System.Threading.Tasks;
using Common.Messages;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace CustomerManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        readonly IPublishEndpoint _publishEndpoint;

        public CustomerController(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        [HttpPost]
        public async Task<ActionResult> Post(CustomerAdded customer)
        {
            await _publishEndpoint.Publish(customer);

            return Ok();
        }
    }

    
}
