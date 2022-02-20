using MassTransit;

namespace Shared.RabbitMq
{
    public static class MassTransitExtensions
    {
        public static async Task PublishAsync<TEvent>(this ConsumeContext context, object values) where TEvent : class
        {
            if (context.RequestId.HasValue)
            {
                await context.RespondAsync<TEvent>(values);
            }

            await context.Publish<TEvent>(values);
        }
    }
}
