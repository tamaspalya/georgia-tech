using EventBus.Messages.Filters.Logging;
using MassTransit;

namespace EventBus.Messages.Filters
{
    public static class FilterExtensions
    {
        public static void UseExceptionLogger<T>(this IPipeConfigurator<T> configurator)
        where T : class, PipeContext
        {
            configurator.AddPipeSpecification(new ExceptionLoggerSpecification<T>());
        }
    }
}
