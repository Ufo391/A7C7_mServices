using MassTransit;
using Microsoft.AspNetCore.SignalR;
using TestApi.Hubs;
using TestTodoService.Sdk.Dtos;
using TestTodoService.Sdk.Events;

namespace TestApi.Events
{
    public class TodoDeletedConsumer : IConsumer<TodoDeleted>
    {
        private readonly ILogger<TodoDeletedConsumer> logger;
        private readonly IHubContext<TodoHub, ITodoClient> todoHub;

        public TodoDeletedConsumer(
            ILogger<TodoDeletedConsumer> logger,
            IHubContext<TodoHub, ITodoClient> todoHub)
        {
            this.logger = logger;
            this.todoHub = todoHub;
            ;
        }

        public async Task Consume(ConsumeContext<TodoDeleted> context)
        {
            try
            {
                if (!context.CorrelationId.HasValue)
                {
                    throw new ArgumentNullException("TodoDeleted: correlation id must not be null. Please check the command trigger.");
                }

                await todoHub.Clients.All.TodoDeleted(context.CorrelationId.Value, context.Message.Id);

                logger.LogInformation($"Todo deleted with id: {context.Message.Id}");
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
            }
        }
    }
}
