using TestPackages.Messages;

namespace TestUserService.Sdk.Events
{
    public interface UserCreated : IEvent
    {
        Guid Id { get; }
        string GivenName { get; }
        string FamilyName { get; }
    }
}
