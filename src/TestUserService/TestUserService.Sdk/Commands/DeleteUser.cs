using TestPackages.Messages;

namespace TestUserService.Sdk.Commands
{
    public interface DeleteUser : ICommand
    {
        Guid Id { get; }
    }
}
