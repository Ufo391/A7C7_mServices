using System;

namespace TestTwoAPI.Sdk.Dtos
{
    [Serializable]
    public class User
    {
        public Guid? Id { get; set; }
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
    }
}
