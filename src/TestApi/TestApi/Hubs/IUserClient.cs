using TestUserService.Sdk.Dtos;

namespace TestApi.Hubs
{
    public interface IUserClient
    {
        Task UserCreated(Guid correlationId, User user);
        Task UserDeleted(Guid correlationId, Guid userId);
    }
}
