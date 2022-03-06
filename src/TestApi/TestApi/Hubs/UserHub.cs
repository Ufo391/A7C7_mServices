using Microsoft.AspNetCore.SignalR;
using TestUserService.Sdk.Dtos;

namespace TestApi.Hubs
{
    public class UserHub : Hub<IUserClient>
    {
        public async Task SendUserCreated(Guid CorrelationId, User user)
        {
            await Clients.All.UserCreated(CorrelationId, user);
        }

        public async Task SendUserDeleted(Guid CorrelationId, Guid userId)
        {
            await Clients.All.UserDeleted(CorrelationId, userId);
        }
    }
}
