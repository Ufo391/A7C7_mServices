using MassTransit;
using TestUserService.Sdk.Commands;

namespace TestUserService.Models.Extensions
{
    public static class Extensions
    {
        public static Sdk.Dtos.User ToDto(this User user)
        {
            return new Sdk.Dtos.User
            {
                Id = user.Id,
                GivenName = user.GivenName,
                FamilyName = user.FamilyName
            };
        }

        public static User ToModel(this Sdk.Dtos.User user)
        {
            return new User
            {
                Id = user.Id ?? NewId.NextGuid(),
                GivenName = user.GivenName,
                FamilyName = user.FamilyName
            };
        }

        public static User ToModel(this CreateUser message)
        {
            return new User
            {
                Id = message.Id ?? NewId.NextGuid(),
                GivenName = message.GivenName,
                FamilyName = message.FamilyName
            };
        }
    }
}
