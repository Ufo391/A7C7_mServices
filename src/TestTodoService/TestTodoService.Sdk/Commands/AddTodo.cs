using TestPackages.Messages;

namespace TestTodoService.Sdk.Commands
{
    public interface AddTodo : ICommand
    {
        Guid? Id { get; }
        string Name { get; }
        string Description { get; }
        bool Done { get; }
        Guid CreatorUserId { get; }
    }
}
