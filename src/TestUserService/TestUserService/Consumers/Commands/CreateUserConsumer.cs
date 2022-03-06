using MassTransit;
using TestUserService.Sdk.Commands;
using TestUserService.Sdk.Events;
using TestUserService.Models;
using TestUserService.Models.Extensions;

namespace TestUserService.Consumers.Commands
{
    public class CreateUserConsumer : IConsumer<CreateUser>
    {
        private readonly ILogger<CreateUserConsumer> logger;
        private readonly UserDbContext userDbContext;

        public CreateUserConsumer(
            ILogger<CreateUserConsumer> logger,
            UserDbContext userDbContext)
        {
            this.logger = logger;
            this.userDbContext = userDbContext;
        }

        public async Task Consume(ConsumeContext<CreateUser> context)
        {
            try
            {
                var user = context.Message.ToModel();

                userDbContext.Users.Add(user);
                await userDbContext.SaveChangesAsync();

                await context.PublishEventAsync<UserCreated>(user);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());

                await context.PublishEventAsync<UserCreationFailed>(new
                {
                    ErrorMessage = ex.ToString()
                });
            }
        }
    }
}
