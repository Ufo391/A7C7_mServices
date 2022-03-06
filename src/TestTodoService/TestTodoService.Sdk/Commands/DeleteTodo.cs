using TestPackages.Messages;

namespace TestTodoService.Sdk.Commands
{
    public interface DeleteTodo : ICommand
    {
        Guid Id { get; }
    }
}
