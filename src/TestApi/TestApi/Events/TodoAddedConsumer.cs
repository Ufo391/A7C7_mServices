using MassTransit;
using Microsoft.AspNetCore.SignalR;
using TestApi.Hubs;
using TestTodoService.Sdk.Dtos;
using TestTodoService.Sdk.Events;

namespace TestApi.Events
{
    public class TodoAddedConsumer : IConsumer<TodoAdded>
    {
        private readonly ILogger<TodoAddedConsumer> logger;
        private readonly IHubContext<TodoHub, ITodoClient> todoHub;

        public TodoAddedConsumer(
            ILogger<TodoAddedConsumer> logger,
            IHubContext<TodoHub, ITodoClient> todoHub)
        {
            this.logger = logger;
            this.todoHub = todoHub;
            ;
        }

        public async Task Consume(ConsumeContext<TodoAdded> context)
        {
            try
            {
                if (!context.CorrelationId.HasValue)
                {
                    throw new ArgumentNullException("TodoAdded: correlation id must not be null. Please check the command trigger.");
                }

                await todoHub.Clients.All.TodoAdded(context.CorrelationId.Value, new Todo
                {
                    Id = context.Message.Id,
                    Name = context.Message.Name,
                    Description = context.Message.Description,
                    Done = context.Message.Done,
                    CreatorUserId = context.Message.CreatorUserId,
                    CreatedAtUtc = context.Message.CreatedAtUtc,
                    UpdatedAtUtc = context.Message.UpdatedAtUtc
                });

                logger.LogInformation($"Todo added: {context.Message.Name}");
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
            }
        }
    }
}
