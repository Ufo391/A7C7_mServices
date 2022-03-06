using TestPackages.Messages;

namespace TestTodoService.Sdk.Events
{
    public interface TodoCompletionFailed : IEvent
    {
        string ErrorCode { get; }
        string ErrorMessage { get; }
    }
}
