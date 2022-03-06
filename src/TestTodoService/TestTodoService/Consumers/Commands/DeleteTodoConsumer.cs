using MassTransit;
using TestTodoService.Models;
using TestTodoService.Sdk.Commands;
using TestTodoService.Sdk.Events;

namespace TestTodoService.Consumers.Commands
{
    public class DeleteTodoConsumer : IConsumer<DeleteTodo>
    {
        private readonly ILogger<DeleteTodoConsumer> logger;
        private readonly TodoDbContext todoDbContext;

        public DeleteTodoConsumer(
            ILogger<DeleteTodoConsumer> logger,
            TodoDbContext todoDbContext)
        {
            this.logger = logger;
            this.todoDbContext = todoDbContext;
        }

        public async Task Consume(ConsumeContext<DeleteTodo> context)
        {
            try
            {
                var entity = await todoDbContext.Todos.FindAsync(context.Message.Id);

                if (entity == null) throw new ArgumentNullException($"Todo to remove not found with id {context.Message.Id}");

                todoDbContext.Todos.Remove(entity);
                await todoDbContext.SaveChangesAsync();

                await context.PublishEventAsync<TodoDeletionFailed>(new
                {
                    context.Message.Id,
                });
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());

                await context.PublishEventAsync<TodoDeletionFailed>(new
                {
                    ErrorMessage = ex.Message
                });
            }
        }
    }
}
