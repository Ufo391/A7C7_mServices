using TestPackages.Messages;

namespace TestUserService.Sdk.Events
{
    public interface UserDeleted : IEvent
    {
        Guid Id { get; }
    }
}
