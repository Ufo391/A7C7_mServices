using TestPackages.Messages;

namespace TestTodoService.Sdk.Commands
{
    public interface CompleteTodo : ICommand
    {
        Guid Id { get; }
    }
}
