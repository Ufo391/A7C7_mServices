using TestPackages.Messages;

namespace TestUserService.Sdk.Commands
{
    public interface CreateUser : ICommand
    {
        Guid? Id { get; }
        string GivenName { get; }
        string FamilyName { get; }
    }
}
