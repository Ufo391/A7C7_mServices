using TestPackages.Messages;

namespace TestTodoService.Sdk.Events
{
    public interface TodoAddingFailed : IEvent
    {
        string ErrorCode { get; }
        string ErrorMessage { get; }
    }
}
