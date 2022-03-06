using MassTransit;
using Microsoft.AspNetCore.SignalR;
using TestApi.Hubs;
using TestTodoService.Sdk.Dtos;
using TestTodoService.Sdk.Events;

namespace TestApi.Events
{
    public class TodoCompletedConsumer : IConsumer<TodoCompleted>
    {
        private readonly ILogger<TodoCompletedConsumer> logger;
        private readonly IHubContext<TodoHub, ITodoClient> todoHub;

        public TodoCompletedConsumer(
            ILogger<TodoCompletedConsumer> logger,
            IHubContext<TodoHub, ITodoClient> todoHub)
        {
            this.logger = logger;
            this.todoHub = todoHub;
            ;
        }

        public async Task Consume(ConsumeContext<TodoCompleted> context)
        {
            try
            {
                if (!context.CorrelationId.HasValue)
                {
                    throw new ArgumentNullException("TodoCompleted: correlation id must not be null. Please check the command trigger.");
                }

                await todoHub.Clients.All.TodoCompleted(context.CorrelationId.Value, context.Message.Id);

                logger.LogInformation($"Todo completed with id: {context.Message.Id}");
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
            }
        }
    }
}
