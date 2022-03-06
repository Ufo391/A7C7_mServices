using TestPackages.Messages;

namespace TestTodoService.Sdk.Events
{
    public interface TodoCompleted : IEvent
    {
        Guid Id { get; }
    }
}
