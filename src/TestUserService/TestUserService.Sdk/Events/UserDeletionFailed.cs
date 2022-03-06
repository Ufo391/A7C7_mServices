using TestPackages.Messages;

namespace TestUserService.Sdk.Events
{
    public interface UserDeletionFailed : IEvent
    {
        string ErrorCode { get; }
        string ErrorMessage { get; }
    }
}
