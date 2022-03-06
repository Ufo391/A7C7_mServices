using Microsoft.AspNetCore.SignalR;
using TestTodoService.Sdk.Dtos;

namespace TestApi.Hubs
{
    public class TodoHub : Hub<ITodoClient>
    {
        public async Task SendTodoAdded(Guid CorrelationId, Todo todo)
        {
            await Clients.All.TodoAdded(CorrelationId, todo);
        }

        public async Task SendTodoDeleted(Guid CorrelationId, Guid todoId)
        {
            await Clients.All.TodoDeleted(CorrelationId, todoId);
        }

        public async Task SendTodoCompleted(Guid CorrelationId, Guid todoId)
        {
            await Clients.All.TodoCompleted(CorrelationId, todoId);
        }
    }
}
