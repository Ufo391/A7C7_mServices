using TestPackages.Messages;

namespace TestUserService.Sdk.Events
{
    public interface UserCreationFailed : IEvent
    {
        string ErrorCode { get; }
        string ErrorMessage { get; }
    }
}
