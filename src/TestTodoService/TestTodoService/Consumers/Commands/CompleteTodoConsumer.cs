using MassTransit;
using TestTodoService.Models;
using TestTodoService.Sdk.Commands;
using TestTodoService.Sdk.Events;

namespace TestTodoService.Consumers.Commands
{
    public class CompleteTodoConsumer : IConsumer<CompleteTodo>
    {
        private readonly ILogger<CompleteTodoConsumer> logger;
        private readonly TodoDbContext todoDbContext;

        public CompleteTodoConsumer(
            ILogger<CompleteTodoConsumer> logger,
            TodoDbContext todoDbContext)
        {
            this.logger = logger;
            this.todoDbContext = todoDbContext;
        }

        public async Task Consume(ConsumeContext<CompleteTodo> context)
        {
            try
            {
                var entity = await todoDbContext.Todos.FindAsync(context.Message.Id);

                if (entity is null) throw new ArgumentNullException($"Todo to complete not found with id {context.Message.Id}");

                entity.Done = true;
                entity.UpdatedAtUtc = DateTime.UtcNow;

                await todoDbContext.SaveChangesAsync();

                await context.PublishEventAsync<TodoCompleted>(new
                {
                    context.Message.Id
                });
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());

                await context.PublishEventAsync<TodoCompletionFailed>(new
                {
                    ErrorMessage = ex.Message
                });
            }
        }
    }
}
