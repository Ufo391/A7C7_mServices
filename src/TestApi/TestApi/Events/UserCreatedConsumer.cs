using MassTransit;
using Microsoft.AspNetCore.SignalR;
using TestApi.Hubs;
using TestUserService.Sdk.Dtos;
using TestUserService.Sdk.Events;

namespace TestApi.Events
{
    public class UserCreatedConsumer : IConsumer<UserCreated>
    {
        private readonly ILogger<UserCreatedConsumer> logger;
        private readonly IHubContext<UserHub, IUserClient> userHub;

        public UserCreatedConsumer(
            ILogger<UserCreatedConsumer> logger,
            IHubContext<UserHub, IUserClient> userHub)
        {
            this.logger = logger;
            this.userHub = userHub;
        }

        public async Task Consume(ConsumeContext<UserCreated> context)
        {
            try
            {
                if (!context.CorrelationId.HasValue)
                {
                    throw new ArgumentNullException("UserCreated: correlation id must not be null. Please check the command trigger.");
                }

                await userHub.Clients.All.UserCreated(context.CorrelationId.Value, new User
                {
                    Id = context.Message.Id,
                    GivenName = context.Message.GivenName,
                    FamilyName = context.Message.FamilyName,
                });

                logger.LogInformation($"User created: {context.Message.GivenName} {context.Message.FamilyName}");
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
            }
        }
    }
}
