using TestTodoService.Sdk.Dtos;

namespace TestApi.Hubs
{
    public interface ITodoClient
    {
        Task TodoAdded(Guid CorrelationId, Todo todo);
        Task TodoDeleted(Guid CorrelationId, Guid todoId);
        Task TodoCompleted(Guid CorrelationId, Guid todoId);
    }
}
