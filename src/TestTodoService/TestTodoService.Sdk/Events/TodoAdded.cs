using TestPackages.Messages;

namespace TestTodoService.Sdk.Events
{
    public interface TodoAdded : IEvent
    {
        Guid Id { get; }
        string Name { get; }
        string Description { get; }
        bool Done { get; }
        Guid CreatorUserId { get; }
        DateTime CreatedAtUtc { get; }
        DateTime UpdatedAtUtc { get; }
    }
}
