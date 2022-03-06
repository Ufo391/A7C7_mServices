using TestPackages.Messages;

namespace TestTodoService.Sdk.Events
{
    public interface TodoDeletionFailed : IEvent
    {
        string ErrorCode { get; }
        string ErrorMessage { get; }
    }
}
