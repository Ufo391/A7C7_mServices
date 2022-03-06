using MassTransit;
using TestUserService.Models;
using TestUserService.Sdk.Commands;
using TestUserService.Sdk.Events;

namespace TestUserService.Consumers.Commands
{
    public class DeleteUserConsumer : IConsumer<DeleteUser>
    {
        private readonly ILogger<DeleteUserConsumer> logger;
        private readonly UserDbContext userDbContext;

        public DeleteUserConsumer(
            ILogger<DeleteUserConsumer> logger,
            UserDbContext userDbContext)
        {
            this.logger = logger;
            this.userDbContext = userDbContext;
        }

        public async Task Consume(ConsumeContext<DeleteUser> context)
        {
            try
            {
                var entity = await userDbContext.Users.FindAsync(context.Message.Id);

                if (entity is null)
                {
                    throw new ArgumentNullException($"User to remove not found with id {context.Message.Id}");
                }

                userDbContext.Users.Remove(entity);
                await userDbContext.SaveChangesAsync();

                await context.PublishEventAsync<UserDeleted>(new
                {
                    context.Message.Id
                });
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());

                await context.PublishEventAsync<UserDeletionFailed>(new
                {
                    ErrorMessage = ex.ToString()
                });
            }
        }
    }
}
