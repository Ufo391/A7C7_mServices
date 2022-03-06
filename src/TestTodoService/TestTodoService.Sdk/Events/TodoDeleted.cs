using TestPackages.Messages;

namespace TestTodoService.Sdk.Events
{
    public interface TodoDeleted : IEvent
    {
        Guid Id { get; }
    }
}
