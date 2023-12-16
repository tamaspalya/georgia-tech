using AutoMapper;
using EventBus.Messages.Events;
using MassTransit;
using MediatR;
using Warehouse.Application.Features.Products.Commands.AddProduct;

namespace Warehouse.API.EventBusConsumer
{
    public class BookAddedConsumer : IConsumer<BookAddedEvent>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<BookAddedConsumer> _logger;

        public BookAddedConsumer(IMediator mediator, IMapper mapper, ILogger<BookAddedConsumer> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Consume(ConsumeContext<BookAddedEvent> context)
        {
            var command = _mapper.Map<AddProductCommand>(context.Message);
            var result = await _mediator.Send(command);

            _logger.LogInformation($"{nameof(AddProductCommand)} consumed successfully. Created Product Id : {result}");
        }
    }
}
