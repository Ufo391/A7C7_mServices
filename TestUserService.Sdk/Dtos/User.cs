using System;

namespace TestUserService.Sdk.Dtos
{
    [Serializable]
    public class User
    {
        public Guid? Id { get; set; }
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
    }
}
