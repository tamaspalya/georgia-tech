using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using EventBus.Messages.Events;

namespace BookManagement.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookManagementController : ControllerBase
    {
        private readonly ILogger<BookManagementController> _logger;
        private readonly IPublishEndpoint _publishEndpoint;

        public BookManagementController(ILogger<BookManagementController> logger, IPublishEndpoint publishEndpoint)
        {
            _logger = logger;
            _publishEndpoint = publishEndpoint;
        }

        [HttpPost(Name = "AddBook")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> AddBook([FromBody] AddBookCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
