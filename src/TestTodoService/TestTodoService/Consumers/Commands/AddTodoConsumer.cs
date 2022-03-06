using MassTransit;
using TestTodoService.Models;
using TestTodoService.Models.Extensions;
using TestTodoService.Sdk.Commands;
using TestTodoService.Sdk.Events;

namespace TestTodoService.Consumers.Commands
{
    public class AddTodoConsumer : IConsumer<AddTodo>
    {
        private readonly ILogger<AddTodoConsumer> logger;
        private readonly TodoDbContext todoDbContext;

        public AddTodoConsumer(
            ILogger<AddTodoConsumer> logger,
            TodoDbContext todoDbContext)
        {
            this.logger = logger;
            this.todoDbContext = todoDbContext;
        }

        public async Task Consume(ConsumeContext<AddTodo> context)
        {
            try
            {
                var entity = context.Message.ToModel();
                todoDbContext.Todos.Add(entity);
                await todoDbContext.SaveChangesAsync();

                await context.PublishEventAsync<TodoAdded>(entity);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());

                await context.PublishEventAsync<TodoAddingFailed>(new
                {
                    ErrorMessage = ex.Message
                });
            }
        }
    }
}
