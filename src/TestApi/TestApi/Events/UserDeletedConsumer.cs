using MassTransit;
using Microsoft.AspNetCore.SignalR;
using TestApi.Hubs;
using TestUserService.Sdk.Dtos;
using TestUserService.Sdk.Events;

namespace TestApi.Events
{
    public class UserDeletedConsumer : IConsumer<UserDeleted>
    {
        private readonly ILogger<UserDeletedConsumer> logger;
        private readonly IHubContext<UserHub, IUserClient> userHub;

        public UserDeletedConsumer(
            ILogger<UserDeletedConsumer> logger,
            IHubContext<UserHub, IUserClient> userHub)
        {
            this.logger = logger;
            this.userHub = userHub;
        }

        public async Task Consume(ConsumeContext<UserDeleted> context)
        {
            try
            {
                if (!context.CorrelationId.HasValue)
                {
                    throw new ArgumentNullException("UserDeleted: correlation id must not be null. Please check the command trigger.");
                }

                await userHub.Clients.All.UserDeleted(context.CorrelationId.Value, context.Message.Id);

                logger.LogInformation($"User deleted with id: {context.Message.Id}");
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
            }
        }
    }
}
