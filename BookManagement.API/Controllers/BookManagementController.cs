using AutoMapper;
using BookManagement.Application.Features.Books.Commands.AddBook;
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
        private readonly IMapper _mapper;

        public BookManagementController(ILogger<BookManagementController> logger, IPublishEndpoint publishEndpoint, IMapper mapper)
        {
            _logger = logger;
            _publishEndpoint = publishEndpoint;
            _mapper = mapper;
        }

        [HttpPost(Name = "AddBook")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> AddBook([FromBody] AddBookCommand command)
        {
            var eventMessage = _mapper.Map<BookAddedEvent>(command);
            await _publishEndpoint.Publish(eventMessage);

            return Accepted();
        }
    }
}
