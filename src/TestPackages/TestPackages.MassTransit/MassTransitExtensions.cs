using MassTransit;
using System.ComponentModel;
using System.Dynamic;
using TestPackages.MassTransit.Helpers;
using TestPackages.Messages;

namespace MassTransit
{
    public static class MassTransitExtensions
    {
        /// <summary>
        /// Sends a command to service bus with values object casted to given command type with generated commandId (which equals correlationId)
        /// </summary>
        /// <typeparam name="TCommand">Type if the Command</typeparam>
        /// <param name="bus"></param>
        /// <param name="values"></param>
        /// <returns>CommandId which equals to CorrelationId if not explicitly set</returns>
        public static async Task<Guid> SendCommandAsync<TCommand>(this IBus bus, object values)
            where TCommand : class, ICommand
        {
            var (command, commandId) = values.CreateCommandMessage<TCommand>();

            await bus.Publish<TCommand>(command);

            return commandId;
        }

        /// <summary>
        /// Publishes an event to service bus and responds requests with it by casting values object to given event type with correlationId from ConsumeContext
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <param name="context"></param>
        /// <param name="values"></param>
        /// <returns>CorrelationId of the event</returns>
        public static async Task<Guid> PublishEventAsync<TEvent>(this ConsumeContext context, object values)
            where TEvent : class, IEvent
        {
            var (@event, correlationId) = values.CreateEventMessage<TEvent>(context);

            if (context.RequestId.HasValue)
            {
                await context.RespondAsync<TEvent>(@event);
            }

            await context.Publish<TEvent>(@event);

            return correlationId;
        }
    }
}
